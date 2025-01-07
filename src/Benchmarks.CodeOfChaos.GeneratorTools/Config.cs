// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class BenchmarkConfig : ManualConfig {
    public BenchmarkConfig() {
        Job job = Job.Default.WithCustomBuildConfiguration("Benchmark");
        AddJob(job);
    }
}