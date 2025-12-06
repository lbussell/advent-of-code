// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.ComponentModel;

namespace AdventOfCode.Solutions.Day06;

internal sealed class Day06Part1() : Solution(2025, 6, 1)
{
    internal static readonly StringSplitOptions Sso = StringSplitOptions.TrimEntries
                                                    | StringSplitOptions.RemoveEmptyEntries;

    public override string Solve(string input)
    {
        var inputLines = input.Split(Environment.NewLine, Sso);

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

    private static char[] ToChars(string input) => input.Split(' ', Sso).Select(s => s[0]).ToArray();
    private static int[] ToInts(string input) => input.Split(' ', Sso).Select(int.Parse).ToArray();

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
        var operations = ParseInput(input);
        var individualResults = operations.Select(o => o.Calculate());
        var result = operations.Select(o => o.Calculate()).Sum().ToString();
        return result;
    }

    private static IEnumerable<Operation> ParseInput(string input)
    {
        var sso = Day06Part1.Sso;

        var operators = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                             .Last()
                             .Split(' ', sso)
                             .Select(s => ParseOperator(s[0]));

        var inputLines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var numberLines = inputLines.SkipLast(1);
        var numbers = ParseNumbers(inputLines);

        return operators.Zip(numbers).Select(o => new Operation(o.First, o.Second));
    }

    private static IEnumerable<long[]> ParseNumbers(string[] input)
    {
        var rows = input.Length;
        var numberLinesCount = rows - 1; // last row is operators
        var columns = input.Max(s => s.Length);

        List<long> currentNumberList = [];
        List<long[]> numbers = [];

        for (int col = 0; col < columns; col += 1)
        {
            // Most significant numbers are on the top,
            // so we need to start from the bottom
            var currentNumber = 0;
            var mult = 1;
            for (int row = numberLinesCount - 1; row >= 0; row -= 1)
            {
                // Our options here should be a single number or a space character.
                // Start with space and then get the actual character if we are
                // within bounds.
                var c = ' ';
                var currentRow = input[row];
                if (col < currentRow.Length)
                    c = currentRow[col];

                if (c != ' ')
                {
                    currentNumber += (c - '0') * mult;
                    mult *= 10;
                }

            }

            // My input did not have 0. So we can safely assume that if we
            // found 0 then we're done with the current math problem.
            if (currentNumber != 0)
            {
                currentNumberList.Add(currentNumber);
            }
            else
            {
                numbers.Add(currentNumberList.ToArray());
                currentNumberList.Clear();
            }
        }

        numbers.Add(currentNumberList.ToArray());
        currentNumberList.Clear();

        return numbers;
    }

    private enum Operator { Add, Multiply }

    private static Operator ParseOperator(char c) => c switch
    {
        '+' => Operator.Add,
        _ => Operator.Multiply,
    };

    private record struct Operation(Operator Operator, long[] Numbers)
    {
        public long Calculate()
        {
            Func<long, long, long> accumulator = Operator switch
            {
                Operator.Add => (a, b) => a + b,
                Operator.Multiply => (a, b) => a * b,
                _ => throw new InvalidOperationException(),
            };

            return Numbers.Aggregate(accumulator);
        }
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day06Part1.Example with { ExpectedOutput = "3263827" }];
}
