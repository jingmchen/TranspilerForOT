// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Data;

namespace TranspilerForOT.Core.Abstractions;

public sealed class ThemeChangedEventArgs(AppTheme theme, AppAccent accent) : EventArgs
{
    public AppTheme Theme { get; } = theme;
    public AppAccent Accent { get; } = accent;
}

public interface IThemeService : IDisposable
{
    AppTheme CurrentTheme { get; }
    AppAccent CurrentAccent { get; }
    event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
    void Initialize();
    void SetTheme(AppTheme theme);
    void SetAccent(AppAccent accent);
    void SetBoth(AppTheme theme, AppAccent accent);
    void ToggleDarkMode();
}