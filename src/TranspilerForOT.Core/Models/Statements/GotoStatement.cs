// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Statements;

public sealed class GotoStatement : Statement
{
    public string Label {get;}

    public GotoStatement(string label)
    {
        Label = label;
    }
}