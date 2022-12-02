using System.Linq;

namespace AdventOfCode;

public class Day01 : Solution
{

    private string input { set; get; }

    public Day01() : base(2022, 01, "Title")
    {
        input = InputLoader.ReadInput($"src/Day01/input.txt");
    }

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

public static class InputLoader
{
    public static string ReadInput(string inputFile)
    {
        return File.ReadAllText(inputFile);
    }
}