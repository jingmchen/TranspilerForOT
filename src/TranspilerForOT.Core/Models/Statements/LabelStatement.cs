// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Statements;

public sealed class LabelStatement : Statement
{
    public string Name {get;}

    public LabelStatement(string name)
        => Name = name;
}