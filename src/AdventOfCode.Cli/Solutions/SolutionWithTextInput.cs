namespace Bussell.AdventOfCode.Solutions;

using System.Runtime.CompilerServices;

public abstract class SolutionWithTextInput : ISolution
{
    public SolutionWithTextInput()
    {
        string basePath = Path.GetDirectoryName(WhereAmI())
            ?? throw new InvalidOperationException("Could not determine base path.");
        Input = File.ReadAllLines(Path.Combine(basePath, InputFileName));
    }

    public abstract int Day { get; }

    public abstract string Name { get; }

    protected string InputFileName => $"../Inputs/{Day:D2}.txt";

    protected IEnumerable<string> Input { get; }

    public abstract string SolvePart1();

    public abstract string SolvePart2();

    private static string WhereAmI([CallerFilePath] string callerFilePath = "") => callerFilePath;
}
