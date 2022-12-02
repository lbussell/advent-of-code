namespace AdventOfCode;

public abstract class Solution
{
    public int Year { get; }
    public int Day { get; }
    public string Title { get; }
    public string input { get; }

    public Solution(int year, int day, string title)
    {
        Year = year;
        Day = day;
        Title = title;
        input = File.ReadAllText($"src/Day{String.Format("{0:D2}", day)}/input.txt");
    }

    public IEnumerable<Result> SolveAndPrintAll()
    {
        IEnumerable<Result> results = SolveAll();

        Console.WriteLine($"Day {Day}: {Title}");

        int part = 0;
        foreach (Result r in results)
        {
            part += 1;
            Console.WriteLine($"Part {part} {r}");
        }

        Console.WriteLine();

        return results;
    }

    public IEnumerable<Result> SolveAll()
    {
        yield return Solve(SolvePartOne);
        yield return Solve(SolvePartTwo);
    }

    public Result Solve(Func<string> SolutionFunction)
    {
        var start = DateTime.Now;
        string answer = SolutionFunction();
        var end = DateTime.Now;
        return new Result(answer, end - start);
    }

    public abstract string SolvePartOne();
    public abstract string SolvePartTwo();
}

public readonly struct Result
{
    public Result(string answer, TimeSpan time)
    {
        this.Answer = answer;
        this.Time = time;
    }

    public override string ToString()
    {
        return $"Solution: {Answer} ({Time.TotalMilliseconds} ms)";
    }

    public string Answer { get; }
    public TimeSpan Time { get; }
}