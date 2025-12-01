// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

public abstract class Solution
{
    protected Solution(int year, int day, int part, string? note = null)
    {
        Year = year;
        Day = day;
        Part = part;
        Note = note;
        DisplayName = note is { Length: > 0 }
            ? $"{year} Day {day} Part {part} ({note})"
            : $"{year} Day {day} Part {part}";
    }

    public int Year { get; }
    public int Day { get; }
    public int Part { get; }
    public string? Note { get; }
    public string DisplayName { get; }

    /// <summary>
    /// Provides example inputs and expected outputs for the solution.
    /// </summary>
    public virtual IEnumerable<Example> Examples { get; } = [];

    public abstract string Solve(string input);
}
