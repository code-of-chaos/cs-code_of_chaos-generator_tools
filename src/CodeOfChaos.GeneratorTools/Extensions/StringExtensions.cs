// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringExtensions {
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
        => string.IsNullOrEmpty(str);

    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? str)
        => !string.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
        => string.IsNullOrWhiteSpace(str);

    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? str)
        => !string.IsNullOrWhiteSpace(str);

    public static string Truncate(this string input, int maxLength) => input.Length <= maxLength ? input : input[..maxLength];
}
