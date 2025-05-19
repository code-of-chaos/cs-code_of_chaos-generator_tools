// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AttributeDataExtensions {
    public static bool IsDisplayName(this AttributeData attribute, string expected) {
        return attribute.AttributeClass?.IsDisplayName(expected) ?? false;
    }
}
