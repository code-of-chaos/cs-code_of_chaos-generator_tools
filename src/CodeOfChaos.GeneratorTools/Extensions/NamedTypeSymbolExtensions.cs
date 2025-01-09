// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class NamedTypeSymbolExtensions {
    public static bool InheritsFrom(this INamedTypeSymbol symbol, INamedTypeSymbol baseType) {
        INamedTypeSymbol? currentSymbol = symbol;
        while (currentSymbol is not null) {
            if (SymbolEqualityComparer.Default.Equals(currentSymbol, baseType)) return true;
            currentSymbol = currentSymbol.BaseType;
        }
        return false;
    }
}
