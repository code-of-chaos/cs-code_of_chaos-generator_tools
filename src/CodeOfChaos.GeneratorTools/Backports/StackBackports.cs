// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
#if NETSTANDARD2_0
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic;
#else
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.GeneratorTools;
#endif

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StackBackports {
    #if NETSTANDARD2_0
    public static bool TryPop<T>(this Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Pop();
        return result is not null;
    }
    
    public static bool TryPeek<T>(this Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Peek();
        return result is not null;
    }
    #endif

    #if NET9_0_OR_GREATER
    public static bool TryPop<T>(Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Pop();
        return result is not null;
    }
    
    public static bool TryPeek<T>(Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Peek();
        return result is not null;
    }
    #endif
}
