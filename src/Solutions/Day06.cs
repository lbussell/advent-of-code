// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Numerics;

namespace AdventOfCode.Solutions.Day06;

internal sealed class Day06Part1() : Solution(2025, 6, 1)
{
    private static readonly StringSplitOptions _sso = StringSplitOptions.TrimEntries
                                                    | StringSplitOptions.RemoveEmptyEntries;

    public override string Solve(string input)
    {
        var inputLines = input.Split(Environment.NewLine, _sso);

        // Last line contains the operators, while all the lines leading up to it contain numbers
        var operators = ToChars(inputLines.Last()).ToArray();
        var numberLines = inputLines.SkipLast(1).Select(ToInts).ToArray();

        long total = 0;

        for (int column = 0; column < operators.Length; column += 1)
        {
            var op = operators[column];
            Func<long, long, long> accumulate = op switch
            {
                '+' => (a, b) => a + b,
                _ => (a, b) => a * b,
            };

            long accum = numberLines[0][column];
            for (int row = 1; row < numberLines.Length; row += 1)
            {
                var nextNumber = numberLines[row][column];
                accum = accumulate(accum, nextNumber);
            }

            total += accum;
        }

        return total.ToString();
    }

    private static char[] ToChars(string input) => input.Split(' ', _sso).Select(s => s[0]).ToArray();
    private static int[] ToInts(string input) => input.Split(' ', _sso).Select(int.Parse).ToArray();

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        123 328  51 64
        45 64  387 23
        6 98  215 314
        *   +   *   +
        """,
        "4277556");
}

internal sealed class Day06Part2() : Solution(2025, 6, 2)
{
    public override string Solve(string input)
    {
        return "";
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day06Part1.Example with { ExpectedOutput = "" }];
}
