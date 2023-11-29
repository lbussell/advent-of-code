namespace Bussell.AdventOfCode.Cli;

using Bussell.AdventOfCode.Solutions;
using Jab;

[ServiceProvider]
[Singleton(typeof(ISolution), typeof(DemoSolution))]
[Singleton(typeof(ISolution), typeof(DemoSolution2))]
internal partial class SolutionProvider
{
}
