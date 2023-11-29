namespace Bussell.AdventOfCode.Solutions;

public abstract class Solution : ISolution
{
    public Solution()
    {
        Input = File.ReadAllLines(InputFileName);
    }

    public abstract int Day { get; }

    public abstract string Name { get; }

    protected string InputFileName => $"Inputs/{Day:D2}.txt";

    protected IEnumerable<string> Input { get; }

    public abstract string SolvePart1();

    public abstract string SolvePart2();
}
