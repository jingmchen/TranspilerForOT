// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Data;

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class UnaryExpression : Expression
{
    public UnaryOperator Operator {get;}
    public Expression Operand {get;}

    public UnaryExpression(UnaryOperator op, Expression operand)
    {
        Operator = op;
        Operand = operand;
    }
}