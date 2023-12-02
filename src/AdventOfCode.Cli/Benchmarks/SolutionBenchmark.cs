namespace Bussell.AdventOfCode.Benchmarks;

using BenchmarkDotNet.Attributes;
using Bussell.AdventOfCode.Cli;
using Bussell.AdventOfCode.Solutions;

[MemoryDiagnoser]
public class SolutionBenchmark
{
    public IEnumerable<ISolution> Solutions
    {
        get
        {
            IConfig config = new Config()
            {
                UseTestInput = false,
            };
            ServiceProvider sp = new() { ConfigInstance = config };
            return sp.GetService<IEnumerable<ISolution>>();
        }
    }

    #pragma warning disable CS8618
    [ParamsSource(nameof(Solutions))]
    public ISolution Solution { get; set; }
    #pragma warning restore CS8618

    [Benchmark]
    public string SolvePart1() => Solution.Solutions[0]();

    [Benchmark]
    public string SolvePart2() => Solution.Solutions[1]();
}
