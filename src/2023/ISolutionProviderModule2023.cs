global using AdventOfCode.Core;

namespace AdventOfCode.Solutions2023;

using Jab;

[ServiceProviderModule]
[Singleton(typeof(ISolution), typeof(Day01))]
[Singleton(typeof(ISolution), typeof(Day02))]
[Singleton(typeof(ISolution), typeof(Day03))]
[Singleton(typeof(ISolution), typeof(Day04))]
[Singleton(typeof(ISolution), typeof(Day05))]
[Singleton(typeof(ISolution), typeof(Day06))]
[Singleton(typeof(ISolution), typeof(Day07))]
[Singleton(typeof(ISolution), typeof(Day08))]
[Singleton(typeof(ISolution), typeof(Day09))]
public interface ISolutionProviderModule2023
{
}
