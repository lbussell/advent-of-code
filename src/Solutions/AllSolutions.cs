// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

using AdventOfCode.Solutions.Day01;
using AdventOfCode.Solutions.Day02;
using AdventOfCode.Solutions.Day03;
using AdventOfCode.Solutions.Day04;

public static class AllSolutions
{
    public static ISolutionCollection Collection => new SolutionCollection([
        new Day01Part1(),
        new Day01Part2(),
        new Day02Part1(),
        new Day02Part2(),
        new Day03Part1(),
        new Day03Part2(),
        new Day04Part1(),
    ]);
}
