// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Data;

// TranspilerForOT.Core.Abstractions.IThemeService
public enum AppTheme {Light, Dark, System}
public enum AppAccent {White, Black}

// Transpiler-related enums
public enum TokenKind
{
    Identifier, Number, Str, Colon, Equals, DoubleEquals, NotEquals, Less, LessOrEqual, Greater, GreaterOrEqual,
    Plus, Minus, Star, Slash, OpenParen, CloseParen, OpenBracket, CloseBracket, OpenBrace, CloseBrace, Comma, Semicolon, Dot, Bang,
    Tilde, // ~
    AmpAmp, // &&
    BarBar, // ||
    Amp, Bar, EndOfLine, EndOfFile, Bad
}
public enum VariableScopeKind {External, Local}
public enum VariableKind {Numeric, Boolean, Byte}
public enum BinaryOperator
{
    Or, And, Equal, NotEqual, Less, LessOrEqual, Greater, GreaterOrEqual, Add, Subtract, Multiply, Divide
}
public enum UnaryOperator {Not, Negate}