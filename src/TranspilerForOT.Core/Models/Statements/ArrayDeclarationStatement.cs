// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Statements;

public sealed class ArrayDeclarationStatement : Statement
{
    public string Name {get;}
    public string Size {get;} // size of array

    public ArrayDeclarationStatement(string name, string size)
    {
        Name = name;
        Size = size;
    }
}