namespace AdventOfCode.Solutions2023;

using AdventOfCode.Core;

public sealed class Day09() : ISolution
{
    public int Year => 2023;

    public int Day => 9;

    public string Name => "OASIS";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private static string SolvePart1(IEnumerable<string> input) => input
            .Select(s => s
                .Split(' ')
                .Select(int.Parse)
                .ToArray())
            .Select(i => Predict(i))
            .Sum()
            .ToString();

    private static string SolvePart2(IEnumerable<string> input) => input
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
            : input.Last() + Predict(diff, reverse);
    }
}
