namespace Bussell.AdventOfCode.Cli;

using BenchmarkDotNet.Running;
using Bussell.AdventOfCode.Benchmarks;

internal class Program
{
    /// <summary>
    /// Entrypoint for my Advent of Code solutions.
    /// </summary>
    /// <param name="day">Which day's solution to run.</param>
    /// <param name="part">Which part of the day's solution to run.</param>
    /// <param name="useTestInput">Whether to use test input or real input.
    /// AoC prohibits uploading real input, so you will need to provide your own under Input/.</param>
    /// <param name="benchmark">Whether to benchmark all solutions (ignores day/part).</param>
    public static void Main(
        int? day = null,
        int? part = null,
        bool useTestInput = false,
        bool benchmark = false)
    {
        IConfig config = new Config()
        {
            Day = day,
            Part = part,
            UseTestInput = useTestInput,
        };

        ServiceProvider sp = new() { ConfigInstance = config };

        if (benchmark)
        {
            BenchmarkRunner.Run(typeof(SolutionBenchmark));
            return;
        }

        ISolver solver = sp.GetService<ISolver>();
        solver.Run();
    }
}
