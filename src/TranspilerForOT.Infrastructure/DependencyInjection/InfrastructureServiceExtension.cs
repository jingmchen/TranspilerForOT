// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using Microsoft.Extensions.DependencyInjection;
using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.Infrastructure.Services;

namespace TranspilerForOT.Infrastructure.DependencyInjection;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppSettingsProvider, AppSettingsProvider>();
        services.AddSingleton<IAppInfo, AppInfo>();
        services.AddSingleton<IAppPaths, AppPaths>();
        return services;
    }
}