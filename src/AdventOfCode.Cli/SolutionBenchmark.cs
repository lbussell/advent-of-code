namespace Bussell.AdventOfCode.Cli;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Bussell.AdventOfCode.Solutions;

[SimpleJob(RunStrategy.Monitoring, launchCount: 0, warmupCount: 0, iterationCount: 1)]
[MemoryDiagnoser]
public class SolutionBenchmark
{
    public IEnumerable<ISolution> Solutions =>
        new SolutionProvider().GetService<IEnumerable<ISolution>>().OrderBy(s => s.Day);

    #pragma warning disable CS8618
    [ParamsSource(nameof(Solutions))]
    public ISolution Solution { get; set; }
    #pragma warning restore CS8618

    [Benchmark]
    public string SolvePart1() => Solution.SolvePart1();

    [Benchmark]
    public string SolvePart2() => Solution.SolvePart2();
}
