// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Models.Expressions;

namespace TranspilerForOT.Core.Models.Statements;

public sealed class IfActionStatement : Statement
{
    public Expression Condition {get;}
    public IReadOnlyList<Statement> ThenAction {get;}
    public IReadOnlyList<Statement>? ElseAction {get;}

    public IfActionStatement(Expression condition, IReadOnlyList<Statement> thenAction, IReadOnlyList<Statement>? elseAction)
    {
        Condition = condition;
        ThenAction = thenAction;
        ElseAction = elseAction;
    }
}