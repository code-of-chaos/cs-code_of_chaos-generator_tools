// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class INamedTypeSymbolExtensions {
    public static string GetTypeKind(this INamedTypeSymbol symbol) {
        return symbol switch {
            { IsRecord: true, TypeKind: TypeKind.Struct } => "record struct",
            { IsRecord: true, TypeKind: TypeKind.Class } => "record",
            { TypeKind: TypeKind.Class } => "class",
            { TypeKind: TypeKind.Struct } => "struct",
            { TypeKind: TypeKind.Interface } => "interface",
            _ => symbol.TypeKind.ToString().ToLowerInvariant()
        };
    }
}
