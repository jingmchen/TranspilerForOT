// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Statements;

public sealed class CallStatement : Statement
{
    public string Name {get;}

    public CallStatement(string name)
    {
        Name = name;
    }
}