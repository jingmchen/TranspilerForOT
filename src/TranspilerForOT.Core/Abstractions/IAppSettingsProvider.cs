// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Configuration;

namespace TranspilerForOT.Core.Abstractions;

public interface IAppSettingsProvider
{
    AppSettings Current {get;}
    void Save();
    void Reload();
}