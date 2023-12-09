using Microsoft.CodeAnalysis.Emit;

namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day8(IConfig config) : SolutionWithTextInput(config)
{
    public override int Day => 8;

    public override string Name => "OASIS";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1() => Input
            .Select(s => s
                .Split(' ')
                .Select(int.Parse)
                .ToArray())
            .Select(i => Predict(i))
            .Sum()
            .ToString();

    private string SolvePart2() => Input
            .Select(s => s
                .Split(' ')
                .Select(int.Parse)
                .ToArray())
            .Select(i => Predict(i, reverse: true))
            .Sum()
            .ToString();

    private static int Predict(int[] input, bool reverse = false)
    {
        if (input.All(i => i == 0))
        {
            return 0;
        }

        int[] diff = new int[input.Length - 1];

        for (int i = 0; i < input.Length - 1; i++)
        {
            diff[i] = input[i + 1] - input[i];
        }

        return reverse
            ? input.First() - Predict(diff, reverse)
            : input.First() + Predict(diff, reverse);
    }
}
