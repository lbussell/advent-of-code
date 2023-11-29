namespace Bussell.AdventOfCode.Cli;

internal class Program
{
    internal static void Main(string[] args)
    {
        Solver solver = new();
        solver.Execute();

        // BenchmarkRunner.Run(typeof(SolutionBenchmark));
    }
}
