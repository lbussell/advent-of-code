namespace Bussell.AdventOfCode.Cli;

using BenchmarkDotNet.Running;

internal class Program
{
    internal static void Main(int? day = null, bool benchmark = false)
    {
        if (benchmark)
        {
            BenchmarkRunner.Run(typeof(SolutionBenchmark));
            return;
        }

        Solver solver = new();

        if (day != null)
        {
            solver.Execute(day.Value);
            return;
        }

        solver.ExecuteAllDays();
    }
}
