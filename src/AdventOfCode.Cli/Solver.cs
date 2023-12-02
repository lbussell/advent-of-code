namespace Bussell.AdventOfCode;

using System.Diagnostics;
using Bussell.AdventOfCode.Solutions;

internal sealed class Solver : ISolver
{
    private readonly IConfig _config;

    private readonly IEnumerable<ISolution> _solutions;

    public Solver(IConfig config, IEnumerable<ISolution> solutions)
    {
        _config = config;
        _solutions = solutions.OrderBy(s => s.Day);
    }

    public void Run()
    {
        if (_config.Day.HasValue)
        {
            SolveSpecificDay(_config.Day.Value, _config.Part);
        }
        else
        {
            SolveAllDays();
        }

        Console.WriteLine();
    }

    private void SolveSpecificDay(int day, int? part)
    {
        IEnumerable<ISolution> solutions = _solutions.Where(s => s.Day == day);

        if (!solutions.Any())
        {
            throw new ArgumentException($"No solution found for day {day}.");
        }

        ExecuteSolution(solutions.First(), part);
    }

    private void SolveAllDays()
    {
        foreach (ISolution s in _solutions)
        {
            ExecuteSolution(s);
        }
    }

    private static void ExecuteSolution(ISolution solution, int? part = null)
    {
        Console.WriteLine($"\nDay {solution.Day:D2} - {solution.Name}");

        if (part.HasValue)
        {
            Console.WriteLine($"\nPart {part}:");
            ExecuteWithTimer(solution.Solutions[part.Value - 1]);
            return;
        }

        for (int i = 0; i < solution.Solutions.Length; i++)
        {
            Console.WriteLine($"\nPart {i + 1}:");
            ExecuteWithTimer(solution.Solutions[i]);
        }
    }

    private static void ExecuteWithTimer(Func<string> runSolution)
    {
        Stopwatch sw = new();

        sw.Start();
        string result = runSolution();
        sw.Stop();

        string elapsedTime = sw.Elapsed.TotalMilliseconds > 1000
            ? $"{sw.Elapsed.TotalSeconds:0.000} s"
            : $"{sw.Elapsed.TotalMilliseconds:0.000} ms";

        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Elapsed: {elapsedTime}");
    }
}
