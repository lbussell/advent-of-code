// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

using AdventOfCode.Solutions.Day01;

public static class AllSolutions
{
    public static ISolutionCollection Collection => new SolutionCollection([
        new Day01Part1(),
        new Day01Part2(),
    ]);
}
