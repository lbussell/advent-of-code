// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

internal sealed class Day01Part1() : Solution(2025, 1, 1)
{
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

    public override string Solve(string input)
    {
        return "foo";
    }
}
