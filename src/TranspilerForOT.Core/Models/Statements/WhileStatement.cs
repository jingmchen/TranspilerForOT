// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Models.Expressions;

namespace TranspilerForOT.Core.Models.Statements;

public sealed class WhileStatement : Statement
{
    public Expression Condition {get;}
    public IReadOnlyList<Statement> Body {get;}

    public WhileStatement(Expression condition, IReadOnlyList<Statement> body)
    {
        Condition = condition;
        Body = body;
    }
}