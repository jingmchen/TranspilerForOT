// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class BoolLiteral : Expression
{
    public bool Value {get;}

    public BoolLiteral(bool value)
        => Value = value;
}