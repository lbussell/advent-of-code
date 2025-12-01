// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions.Day01;

internal sealed record TurnInstruction(TurnDirection Direction, int Steps)
{
    // Example input: "L23" or "R45"
    public static implicit operator TurnInstruction(string input)
    {
        var direction = input[0] switch
        {
            'L' => TurnDirection.Left,
            'R' => TurnDirection.Right,
            _ => throw new ArgumentException("Invalid turn direction", nameof(input))
        };

        if (!int.TryParse(input[1..], out var steps))
        {
            throw new ArgumentException("Invalid step count", nameof(input));
        }

        return new TurnInstruction(direction, steps);
    }
}

internal sealed record Dial(int Position = 50)
{
    private const int TotalPositions = 100;

    public Dial Turn(TurnInstruction instruction) => instruction.Direction switch
    {
        TurnDirection.Left => this with
        {
            Position = (Position - instruction.Steps + TotalPositions) % TotalPositions
        },
        _ => this with
        {
            Position = (Position + instruction.Steps) % TotalPositions
        },
    };
}

internal enum TurnDirection
{
    Left,
    Right
}

internal sealed class Day01Part1() : Solution(2025, 1, 1)
{
    public override string Solve(string input)
    {
        var instructions = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => (TurnInstruction)line);

        Dial[] dialPositions = [new Dial()];

        var allDialPositions = instructions.Aggregate(
            seed: dialPositions,
            func: (accumPositions, instruction) =>
            {
                var lastPosition = accumPositions[^1];
                return [.. accumPositions, lastPosition.Turn(instruction)];
            });

        // The solution is the number of times the dial is left pointing at 0
        // after any rotation in the sequence
        var zeroCount = allDialPositions.Count(dial => dial.Position == 0);
        return zeroCount.ToString();
    }

    public override IEnumerable<Example> Examples { get; } = [
        new Example(
            Input:
                """
                L68
                L30
                R48
                L5
                R60
                L55
                L1
                L99
                R14
                L82
                """,
            ExpectedOutput: "3"
        )
    ];
}
