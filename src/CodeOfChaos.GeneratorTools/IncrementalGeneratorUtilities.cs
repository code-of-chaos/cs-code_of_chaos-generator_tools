// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using System;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class IncrementalGeneratorUtilities {
    [UsedImplicitly] 
    public static IncrementalValueProvider<string> GetAssemblyNamePipeline(IncrementalGeneratorInitializationContext context)
        => context.CompilationProvider.Select(static (compilation, _) => compilation.AssemblyName!);

    [UsedImplicitly] 
    public static IncrementalValuesProvider<AdditionalText> GetRazorFilesPipeline(IncrementalGeneratorInitializationContext context)
        => context.AdditionalTextsProvider.Where(static text => text.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase));
    
}
