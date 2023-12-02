namespace Bussell.AdventOfCode.Cli;

public class Config : IConfig
{
    public int? Day { get; init; }

    public int? Part { get; init; }

    public bool UseTestInput { get; init; } = false;
}
