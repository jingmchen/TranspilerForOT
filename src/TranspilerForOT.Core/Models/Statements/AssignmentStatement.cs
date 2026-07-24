// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Models.Expressions;

namespace TranspilerForOT.Core.Models.Statements;

public sealed class AssignmentStatement : Statement
{
    public Expression Target {get;}
    public Expression Value {get;}

    public AssignmentStatement(Expression target, Expression value)
    {
        Target = target;
        Value = value;
    }
}