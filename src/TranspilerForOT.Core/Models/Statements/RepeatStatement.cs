// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Models.Expressions;

namespace TranspilerForOT.Core.Models.Statements;

public sealed class RepeatStatement : Statement
{
    public Expression Count {get;}
    public IReadOnlyList<Statement> Body {get;}

    public RepeatStatement(Expression count, IReadOnlyList<Statement> body)
    {
        Count = count;
        Body = body;
    }
}