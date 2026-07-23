// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Abstractions;

public interface IAppPaths
{
    string AppDataFolder {get;}
    string LogsFolder {get;}
    string BundledAppSettingsFile {get;}
    string UserAppSettingsFile {get;}
    string LatestLogFile {get;}
}