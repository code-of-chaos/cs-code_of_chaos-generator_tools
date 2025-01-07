// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Benchmarks.CodeOfChaos.GeneratorTools.OldImplementations;
using CodeOfChaos.GeneratorTools;

namespace Benchmarks.CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
[Config(typeof(BenchmarkConfig))]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Testing {
    [Benchmark(Baseline = true, OperationsPerInvoke = 1000)]
    public GeneratorStringBuilder GeneratorStringBuilder_AppendBody_OneLine() {
        var builder = new GeneratorStringBuilder();
        builder.AppendBody("Hello");
        return builder;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public GeneratorStringBuilder GeneratorStringBuilder_AppendBody_MultiLine() {
        var builder = new GeneratorStringBuilder();
        builder.AppendBody("""
            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            Curabitur pharetra turpis et ullamcorper interdum.
            Nunc placerat dolor ac quam gravida, sit amet tempor dui molestie.
            Proin id enim et mauris imperdiet condimentum.

            Nam auctor dui ac efficitur pellentesque.
            Vestibulum ac arcu quis enim auctor sagittis at in nisl.
            Praesent ut libero eu arcu lacinia accumsan non ut purus.

            Ut ut leo ut odio tempus vulputate.
            Sed blandit dui sit amet faucibus elementum.
            Fusce tincidunt mauris in egestas maximus.

            Phasellus in quam vel augue aliquet egestas.
            Aliquam sed ipsum gravida, sagittis velit ut, posuere magna.
            Cras ac dolor venenatis, volutpat nibh nec, commodo diam.
            In at magna ullamcorper tortor aliquet commodo in non elit.
            Integer convallis dolor id gravida dapibus.

            Fusce aliquet nisi ut scelerisque porttitor.
            Quisque ut mauris condimentum, blandit odio at, semper metus.
            Etiam eu mi non enim posuere varius eget ac nibh.
            """);

        return builder;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public GeneratorStringBuilder GeneratorStringBuilder_AppendBodyOld_OneLine() {
        var builder = new GeneratorStringBuilder();
        builder.AppendBodyOld("Hello");
        return builder;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public GeneratorStringBuilder GeneratorStringBuilder_AppendBodyOld_MultiLine() {
        var builder = new GeneratorStringBuilder();
        builder.AppendBodyOld("""
            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            Curabitur pharetra turpis et ullamcorper interdum.
            Nunc placerat dolor ac quam gravida, sit amet tempor dui molestie.
            Proin id enim et mauris imperdiet condimentum.

            Nam auctor dui ac efficitur pellentesque.
            Vestibulum ac arcu quis enim auctor sagittis at in nisl.
            Praesent ut libero eu arcu lacinia accumsan non ut purus.

            Ut ut leo ut odio tempus vulputate.
            Sed blandit dui sit amet faucibus elementum.
            Fusce tincidunt mauris in egestas maximus.

            Phasellus in quam vel augue aliquet egestas.
            Aliquam sed ipsum gravida, sagittis velit ut, posuere magna.
            Cras ac dolor venenatis, volutpat nibh nec, commodo diam.
            In at magna ullamcorper tortor aliquet commodo in non elit.
            Integer convallis dolor id gravida dapibus.

            Fusce aliquet nisi ut scelerisque porttitor.
            Quisque ut mauris condimentum, blandit odio at, semper metus.
            Etiam eu mi non enim posuere varius eget ac nibh.
            """);

        return builder;
    }
}

// | Method                                         | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
// |----------------------------------------------- |----------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
// | GeneratorStringBuilder_AppendBody_OneLine      | 0.0342 ns | 0.0006 ns | 0.0005 ns |  1.00 |    0.02 | 0.0000 |      - |         - |          NA |
// | GeneratorStringBuilder_AppendBodyOld_OneLine   | 0.0817 ns | 0.0009 ns | 0.0007 ns |  2.39 |    0.04 | 0.0000 |      - |         - |          NA |
// | GeneratorStringBuilder_AppendBody_MultiLine    | 1.2728 ns | 0.0093 ns | 0.0082 ns | 37.26 |    0.62 | 0.0004 | 0.0000 |       6 B |          NA |
// | GeneratorStringBuilder_AppendBodyOld_MultiLine | 2.7955 ns | 0.0407 ns | 0.0340 ns | 81.84 |    1.58 | 0.0007 | 0.0000 |      11 B |          NA |
