// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class IndexReference : Expression
{
    public string Name {get;}
    public Expression Index {get;}

    public IndexReference(string name, Expression index)
    {
        Name = name;
        Index = index;
    }
}