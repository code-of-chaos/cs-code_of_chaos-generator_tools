// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;

namespace Benchmarks.CodeOfChaos.GeneratorTools.OldImplementations;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class GeneratorStringBuilder_AppendBodyOld {
    public static GeneratorStringBuilder AppendBodyOld(this GeneratorStringBuilder builder, string text) => builder.BuilderAction(() => {
        string[] lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);

        foreach (string line in lines)
            builder
                .Append(builder.IndentString(builder.IndentAmount))
                .AppendLine(line);
    });
}
