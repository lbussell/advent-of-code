namespace Bussell.AdventOfCode.Solutions;

public interface ISolution
{
    public int Day { get; }

    public string Name { get; }

    public Func<string>[] Solutions { get; }

    // public string SolvePart1();

    // public string SolvePart2();
}
