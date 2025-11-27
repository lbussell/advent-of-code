// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Collections.Frozen;

namespace AdventOfCode.Solutions;

public sealed class SolutionCollection : ISolutionCollection
{
    private readonly Solution[] _all;
    private readonly FrozenDictionary<SolutionQuery, Solution[]> _index;

    public SolutionCollection(IEnumerable<Solution> solutions)
    {
        ArgumentNullException.ThrowIfNull(solutions);

        _all = solutions.ToArray();
        _index = _all.SelectMany(solution => new[]
            {
                (Key: new SolutionQuery(), Value: solution),
                (Key: new SolutionQuery(solution.Year), Value: solution),
                (Key: new SolutionQuery(solution.Year, solution.Day), Value: solution),
                (Key: new SolutionQuery(solution.Year, solution.Day, solution.Part), Value: solution)
            })
            .GroupBy(item => item.Key, item => item.Value)
            .ToFrozenDictionary(group => group.Key, group => group.ToArray());
    }

    public IEnumerable<ISolution> All => _all;

    public IEnumerable<ISolution> Get(SolutionQuery query) =>
        _index.TryGetValue(query, out var matches) ? matches : [];
}
