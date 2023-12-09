namespace Bussell.AdventOfCode;

using Bussell.AdventOfCode.Solutions;
using Jab;

[ServiceProvider]
[Singleton(typeof(IConfig), Instance = nameof(ConfigInstance))]
[Singleton(typeof(ISolver), typeof(Solver))]
[Singleton(typeof(ISolution), typeof(Day1))]
[Singleton(typeof(ISolution), typeof(Day2))]
[Singleton(typeof(ISolution), typeof(Day3))]
[Singleton(typeof(ISolution), typeof(Day4))]
[Singleton(typeof(ISolution), typeof(Day5))]
[Singleton(typeof(ISolution), typeof(Day6))]
[Singleton(typeof(ISolution), typeof(Day7))]
[Singleton(typeof(ISolution), typeof(Day8))]
internal partial class ServiceProvider
{
    public required IConfig ConfigInstance { get; init; }
}
