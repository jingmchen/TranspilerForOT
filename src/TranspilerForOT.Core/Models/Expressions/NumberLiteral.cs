// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class NumberLiteral : Expression
{
    public string Text {get;}

    public NumberLiteral(string text)
        => Text = text;
}