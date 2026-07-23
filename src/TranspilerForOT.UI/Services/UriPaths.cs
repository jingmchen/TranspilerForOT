// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using System.Reflection;
using TranspilerForOT.Core.Abstractions;
using TranspilerForOT.UI.Constants;

namespace TranspilerForOT.UI.Services;

// Lives in UI rather than infrastructure because Assets are bundled in UI
public sealed class UriPaths : IUriPaths
{
    public string ThemeTemplate {get;}
    public string AccentTemplate {get;}
    public string StyleIndex {get;}
    
    public UriPaths(Assembly assembly, IAppInfo appInfo)
    {
        var assemblyName = assembly.GetName().Name ?? appInfo.Product;

        ThemeTemplate =
            $"pack://application,,,/{assemblyName};component/{UIConstants.Bundled.FolderName.Assets};/{UIConstants.Bundled.FolderName.Themes}/{{0}}Theme.xaml";
        
        AccentTemplate =
            $"pack://application,,,/{assemblyName};component/{UIConstants.Bundled.FolderName.Assets}/{UIConstants.Bundled.FolderName.Accents}/{{0}}Accent.xaml";
        
        StyleIndex =
            $"pack://application,,,/{assemblyName};component/{UIConstants.Bundled.FolderName.Assets}/{UIConstants.Bundled.FolderName.Styles}/{UIConstants.Bundled.FileName.StylesIndex}";
    }
}