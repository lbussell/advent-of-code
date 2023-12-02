namespace Bussell.AdventOfCode.Solutions;

using System.Runtime.CompilerServices;

public abstract class SolutionWithTextInput : ISolution
{
    public SolutionWithTextInput(IConfig config)
    {
        string basePath = Path.GetDirectoryName(GetCallerFilePath())
            ?? throw new InvalidOperationException("Could not determine base path.");
        string inputFileName = config.UseTestInput ? $"{Day:D2}.test.txt" : $"{Day:D2}.input.txt";
        Input = File.ReadAllLines(Path.Combine(basePath, "..", "Inputs", inputFileName));
    }

    public abstract int Day { get; }

    public abstract string Name { get; }

    public abstract Func<string>[] Solutions { get; }

    protected IEnumerable<string> Input { get; }

    public override string ToString() => Name;

    private static string GetCallerFilePath([CallerFilePath] string callerFilePath = "") => callerFilePath;
}
