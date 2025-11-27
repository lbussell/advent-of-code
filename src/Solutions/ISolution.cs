// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

public interface ISolution
{
    int Year { get; }
    int Day { get; }
    int Part { get; }
    string DisplayName { get; }
    string Solve(string input);
}
