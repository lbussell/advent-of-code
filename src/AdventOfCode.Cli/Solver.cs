namespace Bussell.AdventOfCode.Cli;

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

    public void Execute()
    {
        foreach (ISolution s in _solutions)
        {
            Console.WriteLine(s.SolvePart1());
            Console.WriteLine(s.SolvePart2());
        }
    }
}
