namespace AdventOfCode;

public class Day01Solution : Solution
{

    public Day01Solution() : base(2022, 1, "Title") { }

    public override string SolvePartOne()
    {
        string[] elves = input.Split(Environment.NewLine + Environment.NewLine);
        return elves.Select(e =>
                e.Split(Environment.NewLine)
                 .Select(int.Parse).Sum())
            .Max().ToString();
    }

    public override string SolvePartTwo()
    {
        string[] elves = input.Split(Environment.NewLine + Environment.NewLine);
        return elves.Select(e =>
                e.Split(Environment.NewLine)
                 .Select(int.Parse).Sum())
            .OrderByDescending(_ => _)
            .Take(3)
            .Sum()
            .ToString();
    }
}
