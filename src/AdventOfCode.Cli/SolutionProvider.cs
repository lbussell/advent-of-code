namespace Bussell.AdventOfCode.Cli;

using Bussell.AdventOfCode.Solutions;
using Jab;

[ServiceProvider]
[Singleton(typeof(ISolution), typeof(Day1))]
internal partial class SolutionProvider
{
}
