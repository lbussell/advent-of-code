// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions.Day05;

internal sealed class Day05Part1() : Solution(2025, 5, 1)
{
    public override string Solve(string input)
    {
        var (freshIdRanges, ingredients) = ParseInput(input);

        var freshIngredients =
            ingredients.Where(ingredient =>
                freshIdRanges.Any(range => range.Contains(ingredient)));

        return freshIngredients.Count().ToString();
    }

    internal static (IEnumerable<InclusiveRange<ulong>>, IEnumerable<ulong>) ParseInput(string input)
    {
        var inputParts = input.Split(
            $"{Environment.NewLine}{Environment.NewLine}",
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var rangeStrings = inputParts[0].Split('\n');
        var idStrings = inputParts[1].Split('\n');

        var ranges = rangeStrings.Select(InclusiveRange<ulong>.Parse);
        var ids = idStrings.Select(ulong.Parse);

        return (ranges, ids);
    }

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        3-5
        10-14
        16-20
        12-18

        1
        5
        8
        11
        17
        32
        """,
        "3");
}

internal sealed class Day05Part2() : Solution(2025, 5, 2)
{
    public override string Solve(string input)
    {
        var (freshIdRanges, _) = Day05Part1.ParseInput(input);

        return freshIdRanges
            .OrderBy(range => range.Start)
            .MergeOverlapping()
            .Select(r => r.Count())
            .Aggregate(0UL, (accum, rangeCount) => accum + rangeCount)
            .ToString();
    }

    public override IEnumerable<Example> Examples { get; } = [Day05Part1.Example with { ExpectedOutput = "14" }];
}

internal readonly record struct InclusiveRange<T>(T Start, T End) where T : IComparable<T>
{
    public bool Contains(T value) => value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
};

internal static class RangeExtensions
{
    extension(InclusiveRange<ulong> range)
    {
        public static InclusiveRange<ulong> Parse(string s)
        {
            var parts = s.Split('-');
            return new InclusiveRange<ulong>(ulong.Parse(parts[0]), ulong.Parse(parts[1]));
        }

        public ulong Count() => range.End - range.Start + 1;
    }

    extension(IEnumerable<InclusiveRange<ulong>> ranges)
    {
        public IEnumerable<InclusiveRange<ulong>> MergeOverlapping()
        {
            ranges = ranges.OrderBy(r => r.Start);
            var currentRange = ranges.Take(1).First();

            foreach (var nextRange in ranges.Skip(1))
            {
                if (currentRange.End >= nextRange.Start)
                {
                    currentRange = new InclusiveRange<ulong>(
                        currentRange.Start,
                        Math.Max(currentRange.End, nextRange.End));
                }
                else
                {
                    yield return currentRange;
                    currentRange = nextRange;
                }
            }

            yield return currentRange;
        }
    }

}
