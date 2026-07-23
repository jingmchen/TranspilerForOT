// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Models.Text;

public sealed class SourceText
{
    private readonly string _text;
    private readonly int[] _lineStarts; // represents an array of line starts; the position right after each new line '\n'
    public int Length => _text.Length;

    public char this[int index] => _text[index]; // Allows return of char quickly eg cls[index]

    private SourceText(string text)
    {
        _text = text;
        _lineStarts = ComputeLineStarts(text);
    }

    public static SourceText From(string text, string fileName = "<memory>")
        => new(text ?? "");
    
    public string ToString(TextSpan span)
        => _text.Substring(span.Start, span.Length);
    
    public LinePosition GetLinePosition(int position)
    {
        // Use binary search to get LinePosition (line number & column number) from _lineStarts[]
        // If position is at start of any line, return element from _lineStarts[]
        // If not, if in first line, return -2, if in second line, return -3, if in third line, return -4 ...
        var line = Array.BinarySearch(_lineStarts, position);
        if (line < 0)
            line = ~line - 1;
        
        int lineNumber = line + 1;
        int columnNumber = position - _lineStarts[line] + 1;

        return new LinePosition(lineNumber, columnNumber);
    }

    private static int[] ComputeLineStarts(string text)
    {
        var starts = new List<int> { 0 };

        for (var i = 0; i < text.Length; i++)
        {
            if (text[i] == '\n')
                starts.Add(i + 1);
        }
        
        return starts.ToArray();
    }
}