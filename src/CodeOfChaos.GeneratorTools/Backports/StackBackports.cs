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
    // Done so we can easily 
    #if NETSTANDARD2_0
    public static bool TryPop<T>(this Stack<T> stack, [NotNullWhen(true)] out T? result) => TryPopBackport(stack, out result);
    #endif
    public static bool TryPopBackport<T>(Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Pop();
        return result is not null;
    }
    
    #if NETSTANDARD2_0
    public static bool TryPeek<T>(this Stack<T> stack, [NotNullWhen(true)] out T? result) => TryPeekBackport(stack, out result);
    #endif
    public static bool TryPeekBackport<T>(Stack<T> stack, [NotNullWhen(true)] out T? result) {
        result = default;
        if (stack.Count == 0) return false;
        result = stack.Peek();
        return result is not null;
    }
}
