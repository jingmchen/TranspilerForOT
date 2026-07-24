// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Statements;

public sealed class TryStatement : Statement
{
    public string FaultVariable {get;}
    public IReadOnlyList<Statement> Body {get;}
    public IReadOnlyList<Statement> Handler {get;}

    public TryStatement(string faultVariable, IReadOnlyList<Statement> body, IReadOnlyList<Statement> handler)
    {
        FaultVariable = faultVariable;
        Body = body;
        Handler = handler;
    }
}