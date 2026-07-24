// Copyright (c) Tan Jing Ming. Use of this software is governed by LICENSE.md.

namespace TranspilerForOT.Core.Data;

// TranspilerForOT.Core.Abstractions.IThemeService
public enum AppTheme {Light, Dark, System}
public enum AppAccent {White, Black}

public enum VariableScopeKind {External, Local}
public enum VariableKind {Numeric, Boolean, Byte}
public enum BinaryOperator {
    Or, And, Equal, NotEqual, Less, LessOrEqual, Greater, GreaterOrEqual, Add, Subtract, Multiply, Divide
}
public enum UnaryOperator {Not, Negate}
