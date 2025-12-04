// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions.Day03;

internal sealed class Day03Part1() : Solution(2025, 3, 1)
{
    private readonly char[] _largestTwo = ['0', '0'];

    public override string Solve(string input)
    {
        var total = 0;
        var banks = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var bank in banks)
            total += LargestCombination2(bank);

        return total.ToString();
    }

    private static int LargestCombination2(ReadOnlySpan<char> bank)
    {
        var leftIndex = 0;
        for (int i = 0; i < bank.Length - 1; i++)
            if (bank[i] > bank[leftIndex])
                leftIndex = i;

        var rightIndex = leftIndex + 1;
        for (int i = rightIndex; i < bank.Length; i++)
            if (bank[i] > bank[rightIndex])
                rightIndex = i;

        return int.Parse([bank[leftIndex], bank[rightIndex]]);
    }

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        987654321111111
        811111111111119
        234234234234278
        818181911112111
        """,
        "357");
}
