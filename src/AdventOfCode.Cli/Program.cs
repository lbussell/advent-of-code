namespace Bussell.AdventOfCode.Cli;

using BenchmarkDotNet.Running;

internal class Program
{
    /// <summary>
    /// Entrypoint for my Advent of Code solutions.
    /// </summary>
    /// <param name="day">Which day's problem solutions to run.</param>
    /// <param name="benchmark">Whether to benchmark all solutions.</param>
    public static void Main(int? day = null, bool benchmark = false)
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
