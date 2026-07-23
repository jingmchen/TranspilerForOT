// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Text;

public readonly record struct TextSpan : IEquatable<TextSpan>
{
    public int Start {get;}
    public int Length {get;}
    public int End => Start + Length;

    public TextSpan(int start, int length)
    {
        Start = start;
        Length = length;
    }

    public static TextSpan FromBounds(int start, int end)
        => new(start, end - start);
    
    public bool Equals(TextSpan other)
        => other.Start == Start && other.Length == Length;

    public override int GetHashCode()
        => HashCode.Combine(Start, Length);
}