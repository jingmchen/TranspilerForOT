// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using Microsoft.Extensions.DependencyInjection;
using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.UI.Services;
using TranspilerForOT.UI.ViewModels;
using TranspilerForOT.UI.Views;

namespace TranspilerForOT.UI.DependencyInjection;

public static class UIServices
{
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        AddServices(services);
        services.AddSingleton<App>();
        AddViewsAndViewModels(services);
        return services;
    }

    private static IServiceCollection AddViewsAndViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IUriPaths, UriPaths>();
        services.AddSingleton<IThemeService, ThemeService>();
        return services;
    }
}