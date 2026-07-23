// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

using TranspilerForOT.Core.Models.Text;

namespace TranspilerForOT.Core.Models.Syntax;

public abstract class SyntaxNode
{
    public static readonly IReadOnlyDictionary<string, string> NoCaptures =
        new Dictionary<string, string>();
    
    public TextSpan Span {get; init;}

    public IReadOnlyList<string> LeadingComments {get; init;} = [];
    public string? TrailingComments {get; init;}
}