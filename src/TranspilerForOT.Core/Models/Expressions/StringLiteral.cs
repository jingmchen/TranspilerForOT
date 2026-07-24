// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class StringLiteral : Expression
{
    public string RawText {get;}

    public StringLiteral(string rawText)
        => RawText = rawText;
}