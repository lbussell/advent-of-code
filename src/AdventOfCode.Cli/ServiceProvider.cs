namespace Bussell.AdventOfCode.Cli;

using Bussell.AdventOfCode.Solutions;
using Jab;

[ServiceProvider]
[Singleton(typeof(IConfig), Instance = nameof(ConfigInstance))]
[Singleton(typeof(ISolver), typeof(Solver))]
[Singleton(typeof(ISolution), typeof(Day1))]
internal partial class ServiceProvider
{
    public required IConfig ConfigInstance { get; init; }
}
