// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using Serilog.Events;
using TranspilerForOT.Core.Data;

namespace TranspilerForOT.Core.Configuration;

public sealed class AppSettings
{
    public LoggingSettings LoggingSection { get; set; } = new();
    public ThemeSettings ThemeSection { get; set; } = new();
}

public sealed record LoggingSettings
{
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    public int RetainedFileCountLimit { get; set; } = 7;
}

public sealed record ThemeSettings
{
    public AppTheme Theme { get; set; } = AppTheme.Light;
    public AppAccent Accent { get; set; } = AppAccent.Black;
}