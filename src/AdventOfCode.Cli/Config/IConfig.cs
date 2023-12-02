namespace Bussell.AdventOfCode;

public interface IConfig
{
    int? Day { get; init; }

    int? Part { get; init; }

    bool UseTestInput { get; init; }
}
