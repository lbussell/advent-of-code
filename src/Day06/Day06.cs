namespace AdventOfCode;

public class Day06Solution : Solution
{

    public Day06Solution() : base(2022, 6, "Tuning Trouble") { }

    public override string SolvePartOne()
    {
        char[] inputLine = input.Split(Environment.NewLine).FirstOrDefault()!.ToCharArray();
        return Solve(inputLine, 4);
    }

    private string Solve(char[] input, int windowSize)
    {
        IEnumerable<IEnumerable<char>> windows = input.Select((_, i) => GetWindow<char>(input, i, windowSize));
        foreach (var (window, i) in windows.Select((w, i) => (w, i)))
        {
            if (window.Distinct().Count() == windowSize)
                return i.ToString();
        }
        throw new Exception("Didn't find a distinct string of chars");
    }

    public override string SolvePartTwo()
    {
        char[] inputLine = input.Split(Environment.NewLine).FirstOrDefault()!.ToCharArray();
        return Solve(inputLine, 14);
    }

    private static IEnumerable<T> GetWindow<T>(T[] l, int index, int windowSize)
    {
        if (index >= l.Count())
            throw new ArgumentOutOfRangeException($"Index {l} is out of range for array {nameof(l)}.");
        
        return l.Skip(index - windowSize).Take(Math.Min(index, windowSize));
    }
}