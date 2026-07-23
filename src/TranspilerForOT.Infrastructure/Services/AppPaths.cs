// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.Infrastructure.Constants;

namespace TranspilerForOT.Infrastructure.Services;

public sealed class AppPaths : IAppPaths
{
    public string AppDataFolder {get;}
    public string LogsFolder {get;}
    public string BundledAppSettingsFile {get;}
    public string UserAppSettingsFile {get;}
    public string LatestLogFile {get;}

    public AppPaths(IAppInfo appInfo)
    {
        AppDataFolder =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                appInfo.Product
            );
        
        LogsFolder =
            Path.Combine(
                AppDataFolder,
                InfrastructureConstants.UserData.FolderName.Logs
            );
        
        BundledAppSettingsFile =
            Path.Combine(
                AppContext.BaseDirectory,
                InfrastructureConstants.Bundled.FileName.AppSettings
            );
        
        UserAppSettingsFile =
            Path.Combine(
                AppDataFolder,
                InfrastructureConstants.UserData.FileName.AppSettings
            );
        
        LatestLogFile =
            Path.Combine(
                LogsFolder,
                InfrastructureConstants.UserData.FileName.LatestLog
            );
    }
}