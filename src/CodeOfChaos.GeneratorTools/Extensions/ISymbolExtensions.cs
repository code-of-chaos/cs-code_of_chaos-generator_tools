// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class ISymbolExtensions {
    public static bool IsDisplayName<TSymbol>(this TSymbol? symbol, string expected) where TSymbol : ISymbol {
        if (symbol == null) return false;
        return symbol.ToDisplayString() == expected;
    }
}
