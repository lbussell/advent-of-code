namespace Bussell.AdventOfCode.Solutions;

using System.Runtime.CompilerServices;

public abstract class SolutionWithTextInput : ISolution
{
    public SolutionWithTextInput()
    {
        string basePath = Path.GetDirectoryName(GetCallerFilePath())
            ?? throw new InvalidOperationException("Could not determine base path.");
        Input = File.ReadAllLines(Path.Combine(basePath, InputFileName));
    }

    public abstract int Day { get; }

    public abstract string Name { get; }

    public abstract Func<string>[] Solutions { get; }

    protected IEnumerable<string> Input { get; }

    private string InputFileName => $"../Inputs/{Day:D2}.input.txt";

    public override string ToString() => Name;

    private static string GetCallerFilePath([CallerFilePath] string callerFilePath = "") => callerFilePath;
}
