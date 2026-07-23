// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.Core.Configuration;
using TranspilerForOT.Core.Data;

namespace TranspilerForOT.Infrastructure.Services;

public sealed partial class AppSettingsProvider : IAppSettingsProvider
{
    public AppSettings Current { get; private set; } = null!;
    private readonly ILogger<AppSettingsProvider> _logger;
    private readonly object _gate = new();
    private readonly string _settingsPath;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = {new JsonStringEnumConverter()}
    };
    private const int MinRetainedFiles = 1;
    private const int MaxRetainedFiles = 30;

    public AppSettingsProvider(ILogger<AppSettingsProvider> logger, string settingsPath)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentException.ThrowIfNullOrEmpty(settingsPath);

        _logger = logger;
        _settingsPath = settingsPath;

        var dir = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);
        
        Reload();
    }

    // ─── PUBLIC METHODS ────────────────────────
    public void Save()
    {
        lock(_gate)
            WriteToDesk(Current);
    }

    public void Reload()
    {
        lock(_gate)
            ReloadCore();
    }

    // ─── PRIVATE METHODS ───────────────────────
    private void ReloadCore()
    {
        if (!File.Exists(_settingsPath))
        {
            LogCreatingDefaults();
            ApplyDefaults();
            return;
        }

        try
        {
            var json = File.ReadAllText(_settingsPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json, _jsonOptions);

            if (settings is null)
            {
                LogInvalidContent();
                ApplyDefaults();
                return;
            }

            Current = Sanitize(settings);
            TrySave();
        }
        catch (Exception ex)
        {
            LogUnableToLoad(ex);
            ApplyDefaults();
        }
    }

    private void ApplyDefaults()
    {
        Current = new AppSettings();
        Save();
    }

    private void TrySave()
    {
        try
        {
            WriteToDesk(Current);
        }
        catch (Exception ex)
        {
            LogUnableToSave(ex);
        }
    }

    private void WriteToDesk(AppSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, _jsonOptions);
        var tmp = _settingsPath + ".tmp";

        try
        {
            File.WriteAllText(tmp, json);
            File.Move(tmp, _settingsPath, overwrite: true);
        }
        catch
        {
            TryDelete(tmp);
            throw;
        }
    }

    private void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch (Exception ex)
        {
            LogTempCleanupFailed(ex, path);
        }
    }

    private static AppSettings Sanitize(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        settings.LoggingSection ??= new();
        settings.ThemeSection ??= new();

        if (!Enum.IsDefined(settings.LoggingSection.MinimumLevel))
            settings.LoggingSection.MinimumLevel = LogEventLevel.Information;
        
        settings.LoggingSection.RetainedFileCountLimit =
            Math.Clamp(settings.LoggingSection.RetainedFileCountLimit, MinRetainedFiles, MaxRetainedFiles);
        
        if (!Enum.IsDefined(settings.ThemeSection.Theme))
            settings.ThemeSection.Theme = AppTheme.Light;
        
        if (!Enum.IsDefined(settings.ThemeSection.Accent))
            settings.ThemeSection.Accent = AppAccent.Black;
        
        return settings;
    }

    [LoggerMessage(
        EventId = 1001, Level = LogLevel.Information, Message = "No user appsettings file found - reverting to factory defaults."
    )]
    private partial void LogCreatingDefaults();

    [LoggerMessage(
        EventId = 1002, Level = LogLevel.Warning, Message = "Unable to read appsettings file - reverting to factory defaults."
    )]
    private partial void LogUnableToLoad(Exception ex);

    [LoggerMessage(
        EventId = 1003, Level = LogLevel.Warning, Message = "Appsettings file was empty or invalid - reverting to factory defaults."
    )]
    private partial void LogInvalidContent();

    [LoggerMessage(
        EventId = 1004, Level = LogLevel.Warning, Message = "Unable to persist appsettings file."
    )]
    private partial void LogUnableToSave(Exception ex);

    [LoggerMessage(
        EventId = 1005, Level = LogLevel.Debug, Message = "Could not remove temporary settings file '{Path}'; it will be overwritten on the next successful save."
    )]
    private partial void LogTempCleanupFailed(Exception ex, string path);
}