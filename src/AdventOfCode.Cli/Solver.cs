namespace Bussell.AdventOfCode.Cli;

using System.Diagnostics;
using Bussell.AdventOfCode.Solutions;

internal sealed class Solver
{
    private readonly IEnumerable<ISolution> _solutions;

    public Solver()
    {
        SolutionProvider sp = new();
        _solutions = sp.GetService<IEnumerable<ISolution>>().OrderBy(s => s.Day);
        Console.WriteLine($"Found {_solutions.Count()} solutions.");
    }

    public static void ExecuteSolution(ISolution solution)
    {
        Console.WriteLine($"Day {solution.Day:D2} - {solution.Name}");
        ExecuteWithTimer(solution.SolvePart1);
        ExecuteWithTimer(solution.SolvePart2);
    }

    public static void ExecuteWithTimer(Func<string> runSolution)
    {
        Stopwatch sw = new();
        sw.Start();
        string result = runSolution();
        sw.Stop();
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Elapsed: {sw.Elapsed}");
    }

    public void Execute(int day)
    {
        ExecuteSolution(_solutions.Where(s => s.Day == day).First());
    }

    public void ExecuteAllDays()
    {
        foreach (ISolution s in _solutions)
        {
            ExecuteSolution(s);
        }
    }
}
