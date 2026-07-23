// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Text;

public sealed class LinePosition
{
    public int Line {get;}
    public int Column {get;}

    public LinePosition(int line, int column)
    {
        Line = line;
        Column = column;
    }
}