// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using System.Globalization;
using System.Text;
using System.Windows;
using Microsoft.Extensions.Logging;
using Windows.UI.ViewManagement;
using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.Core.Data;

namespace TranspilerForOT.UI.Services;

public sealed class ThemeService : IThemeService
{
    public AppTheme CurrentTheme {get; private set;}
    public AppAccent CurrentAccent {get; private set;}
    public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
    private readonly ILogger<ThemeService> _logger;
    private readonly IAppSettingsProvider _settings;
    private ResourceDictionary? _themeSlot;
    private ResourceDictionary? _accentSlot;
    private readonly Dictionary<AppTheme, ResourceDictionary> _themeCache = [];
    private readonly Dictionary<AppAccent, ResourceDictionary> _accentCache = [];
    private readonly UISettings _uiSettings = new();
    private bool _isInitialized;
    private bool _isDisposed;
    private readonly CompositeFormat _themeTemplate;
    private readonly CompositeFormat _accentTemplate;
    
    public ThemeService(ILogger<ThemeService> logger, IAppSettingsProvider settings, IUriPaths uriPaths)
    {
        _logger = logger;
        _settings = settings;
        _themeTemplate = CompositeFormat.Parse(uriPaths.ThemeTemplate);
        _accentTemplate = CompositeFormat.Parse(uriPaths.AccentTemplate);
    }

    // ─── PUBLIC METHODS ────────────────────────
    public void Initialize()
    {
        if (_isInitialized)
        {
            _logger.LogWarning("{ThemeService} is already initialized.", nameof(ThemeService));
            return;
        }

        ThrowIfAppNotReady();

        CurrentTheme = _settings.Current.ThemeSection.Theme;
        CurrentAccent = _settings.Current.ThemeSection.Accent;

        _themeSlot = new ResourceDictionary();
        _accentSlot = new ResourceDictionary();

        var merged = Application.Current!.Resources.MergedDictionaries;

        merged.Add(_themeSlot);
        merged.Add(_accentSlot);
        
        ApplyCore(CurrentTheme, CurrentAccent, fireEvent: false, persist: true);

        // Subscribe to OS theme changes
        _uiSettings.ColorValuesChanged += OnSystemThemeChanged;

        _isInitialized = true;
    }

    public void SetTheme(AppTheme theme)
        => SetBoth(theme, CurrentAccent);
    
    public void SetAccent(AppAccent accent)
        => SetBoth(CurrentTheme, accent);
    
    public void SetBoth(AppTheme theme, AppAccent accent)
    {
        ThrowIfNotInitialized();
        ThrowIfDisposed();

        if (theme == CurrentTheme && accent == CurrentAccent) return;

        InvokeOnUiThread(() => ApplyCore(theme, accent, fireEvent: true, persist: true));
    }

    public void ToggleDarkMode()
    {
        var effectiveTheme = CurrentTheme == AppTheme.System ? GetSystemTheme() : CurrentTheme;
        SetTheme(effectiveTheme == AppTheme.Dark ?  AppTheme.Light : AppTheme.Dark);
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        _uiSettings.ColorValuesChanged -= OnSystemThemeChanged;
    }

    // ─── PRIVATE METHODS ───────────────────────
    // Core implementation
    private void ApplyCore(AppTheme theme, AppAccent accent, bool fireEvent, bool persist = true)
    {
        if (_isDisposed) return;

        CurrentTheme = theme;
        CurrentAccent = accent;
        
        var effectiveTheme = theme == AppTheme.System ? GetSystemTheme() : theme;

        // Fluent theme for built-in control styles
        Application.Current!.ThemeMode =
            effectiveTheme == AppTheme.Dark ? ThemeMode.Dark : ThemeMode.Light;
        
        var merged = Application.Current!.Resources.MergedDictionaries;

        _themeSlot?.MergedDictionaries.Clear();
        _themeSlot?.MergedDictionaries.Add(
            GetOrLoadDictionary(_themeCache, effectiveTheme, ThemeUri)
        );

        _accentSlot?.MergedDictionaries.Clear();
        _accentSlot?.MergedDictionaries.Add(
            GetOrLoadDictionary(_accentCache, CurrentAccent, AccentUri)
        );

        if (persist)
            Persist();

        if (fireEvent)
            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(theme, accent));
    }

    private void Persist()
    {
        _settings.Current.ThemeSection.Theme = CurrentTheme;
        _settings.Current.ThemeSection.Accent = CurrentAccent;
        _settings.Save();
    }

    private static void InvokeOnUiThread(Action action)
    {
        var dispatcher = Application.Current!.Dispatcher;

        if (dispatcher.CheckAccess())
            action();
        else
            dispatcher.BeginInvoke(() => action());
    }

    // System theme listener
    private void OnSystemThemeChanged(UISettings sender, object args)
    {
        if (_isDisposed || CurrentTheme != AppTheme.System) return;

        InvokeOnUiThread(() => ApplyCore(AppTheme.System, CurrentAccent, fireEvent: true, persist: true));
    }

    private AppTheme GetSystemTheme()
        => IsColorLight(_uiSettings.GetColorValue(UIColorType.Background))
            ? AppTheme.Light
            : AppTheme.Dark;
    
    private static bool IsColorLight(Windows.UI.Color c)
        => (5 * c.G + 2 * c.R + c.B) > 8 * 128;

    // Cache helpers
    private static ResourceDictionary GetOrLoadDictionary<TKey>(
        Dictionary<TKey, ResourceDictionary> cache,
        TKey key,
        Func<TKey, Uri> uriFactory
    ) where TKey : notnull
    {
        if (!cache.TryGetValue(key, out var dict))
        {
            dict = LoadDictionary(uriFactory(key));
            cache[key] = dict;
        }
        return dict;
    }

    private static ResourceDictionary LoadDictionary(Uri uri)
        => new() {Source = uri};

    // Uri helpers
    private Uri ThemeUri(AppTheme theme)
        => new(string.Format(CultureInfo.InvariantCulture, _themeTemplate, theme));
    
    private Uri AccentUri(AppAccent accent)
        => new(string.Format(CultureInfo.InvariantCulture, _accentTemplate, accent));


    // Guard
    private void ThrowIfNotInitialized()
    {
        if (!_isInitialized)
            throw new InvalidOperationException($"{nameof(ThemeService)} is not initialized yet.");
    }

    private static void ThrowIfAppNotReady()
    {
        if (Application.Current is null)
            throw new InvalidOperationException("Application is not yet ready.");
    }

    private void ThrowIfDisposed()
        => ObjectDisposedException.ThrowIf(_isDisposed, nameof(ThemeService));
}