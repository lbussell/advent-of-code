// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions.Day03;

internal sealed class Day03Part1() : Solution(2025, 3, 1)
{
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

internal sealed class Day03Part2() : Solution(2025, 3, 2)
{
    private const int BatteriesToUse = 12;
    private readonly Stack<char> _batteryStack = new(BatteriesToUse);

    public override string Solve(string input)
    {
        var banks = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        long total = 0;
        foreach (var bank in banks) total += LargestCombination(bank);

        return total.ToString();
    }

    private long LargestCombination(ReadOnlySpan<char> bank)
    {
        var numberOfBatteries = bank.Length;

        _batteryStack.Clear();
        _batteryStack.Push(bank[0]);

        for (var i = 1; i < numberOfBatteries; i += 1)
        {
            var currentBatteryValue = bank[i];

            while (_batteryStack.Count > 0
                   && (numberOfBatteries - i + _batteryStack.Count) > BatteriesToUse
                   && currentBatteryValue > _batteryStack.Peek())
                _batteryStack.Pop();

            if (_batteryStack.Count < BatteriesToUse)
                _batteryStack.Push(currentBatteryValue);
        }

        var combination = long.Parse(_batteryStack.Reverse().ToArray());
        return combination;
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day03Part1.Example with { ExpectedOutput = "3121910778619" }];
}
