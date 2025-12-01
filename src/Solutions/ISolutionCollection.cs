// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Solutions;

public interface ISolutionCollection
{
    IEnumerable<Solution> All { get; }
    IEnumerable<Solution> Get(SolutionQuery query);
}
