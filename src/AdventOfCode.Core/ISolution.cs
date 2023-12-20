namespace AdventOfCode.Core;

public interface ISolution
{
    public int Year { get; }

    public int Day { get; }

    public string Name { get; }

    Func<IEnumerable<string>, string>[] Solutions { get; }
}
