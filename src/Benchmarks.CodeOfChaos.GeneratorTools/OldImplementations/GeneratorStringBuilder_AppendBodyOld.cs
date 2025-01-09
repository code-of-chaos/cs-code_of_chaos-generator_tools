// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;

namespace Benchmarks.CodeOfChaos.GeneratorTools.OldImplementations;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class GeneratorStringBuilder_AppendBodyOld {
    public static GeneratorStringBuilder AppendBodyOld(this GeneratorStringBuilder builder, string text) {
        string[] lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);

        foreach (string line in lines) {
            builder.Indent(g => g.AppendLine(line));
        }

        return builder;
    }
}
