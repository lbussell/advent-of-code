// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

public record SolutionQuery(int Year, int Day, int Part)
{
    public SolutionQuery() : this(-1, -1, -1) { }
    public SolutionQuery(int year) : this(year, -1, -1) { }
    public SolutionQuery(int year, int day) : this(year, day, -1) { }
};
