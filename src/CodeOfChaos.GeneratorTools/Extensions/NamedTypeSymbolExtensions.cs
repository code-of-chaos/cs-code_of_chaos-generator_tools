// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class NamedTypeSymbolExtensions {
    public static bool InheritsFrom(this INamedTypeSymbol symbol, INamedTypeSymbol baseType) {
        Stack<INamedTypeSymbol> stack = new();
        stack.Push(symbol);
        
        while (stack.Count != 0) {
            INamedTypeSymbol? currentSymbol = stack.Pop();
            if (SymbolEqualityComparer.Default.Equals(currentSymbol, baseType)) return true;
            foreach (INamedTypeSymbol @interface in currentSymbol.AllInterfaces) stack.Push(@interface);
            if (currentSymbol.BaseType == null) continue;
            stack.Push(currentSymbol.BaseType);
        }
        return false;
    }
}
