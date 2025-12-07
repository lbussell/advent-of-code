// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Text;

namespace AdventOfCode.Solutions.Day07;

internal record struct Point(int Row, int Col)
{
    public readonly int Y => Row;
    public readonly int X => Col;
};

internal sealed class Day07Part1() : Solution(2025, 7, 1)
{
    internal static readonly StringSplitOptions Sso = StringSplitOptions.TrimEntries
                                                    | StringSplitOptions.RemoveEmptyEntries;

    private char[][] _manifold = [];
    private bool[][] _visited = [];

    public override string Solve(string input)
    {
        _manifold = input.Split(Environment.NewLine, Sso)
                         .Select(line => line.ToCharArray())
                         .ToArray();

        // Initialize visited array to keep track of visited points
        _visited = new bool[_manifold.Length][];
        for (int i = 0; i < _manifold.Length; i++)
            _visited[i] = new bool[_manifold[i].Length];

        // Start right below the S character
        var startIndex = _manifold[0].IndexOf('S');
        var start = new Point(Row: 1, Col: startIndex);
        var result = GetSplits(start);
        return result.ToString();
    }

    private int GetSplits(Point start)
    {
        // If we've already visited this point then stop counting
        if (Visited(start)) return 0;

        // Move down until we hit a '^' character or the bottom of the map
        var current = start;
        while (_manifold[current.Row][current.Col] != '^')
        {
            MarkVisited(current);
            current = new Point(current.Row + 1, current.Col);
            // If we hit the bottom of the map, then stop counting
            if (current.Row == _manifold.Length) return 0;
        }

        // If we made it here, then we hit a splitter (^)
        // Spawn two new recursive paths, one to the left and one to the right
        var result = 0;

        var left = new Point(current.Row, current.Col - 1);
        var leftVisited = Visited(left);
        if (!leftVisited) result += GetSplits(left);

        var right = new Point(current.Row, current.Col + 1);
        var rightVisited = Visited(right);
        if (!rightVisited) result += GetSplits(right);

        if (!leftVisited || !rightVisited) result += 1;

        return result;
    }

    private void MarkVisited(Point point) => _visited[point.Row][point.Col] = true;
    private bool Visited(Point point) => _visited[point.Row][point.Col];

    public void Print()
    {
        var output = new StringBuilder();
        for (int i = 0; i < _manifold.Length; i++)
        {
            for (int j = 0; j < _manifold[i].Length; j++)
                output.Append(_visited[i][j] ? '|' : _manifold[i][j]);
            output.AppendLine();
        }

        Console.WriteLine(output.ToString());
    }

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        .......S.......
        ...............
        .......^.......
        ...............
        ......^.^......
        ...............
        .....^.^.^.....
        ...............
        ....^.^...^....
        ...............
        ...^.^...^.^...
        ...............
        ..^...^.....^..
        ...............
        .^.^.^.^.^...^.
        ...............
        """,
        "21");
}

internal sealed class Day07Part2() : Solution(2025, 7, 2)
{
    internal static readonly StringSplitOptions Sso = StringSplitOptions.TrimEntries
                                                    | StringSplitOptions.RemoveEmptyEntries;

    private char[][] _manifold = [];
    private readonly Dictionary<Point, long> _cache = [];

    public override string Solve(string input)
    {
        _manifold = input.Split(Environment.NewLine, Sso)
                         .Select(line => line.ToCharArray())
                         .ToArray();

        // Start right below the S character
        var startIndex = _manifold[0].IndexOf('S');
        var start = new Point(Row: 1, Col: startIndex);
        var result = GetSplits(start);
        return result.ToString();
    }

    private long GetSplits(Point start)
    {
        // Move down until we hit a '^' character or the bottom of the map
        var current = start;
        while (_manifold[current.Row][current.Col] != '^')
        {
            current = new Point(current.Row + 1, current.Col);
            // If we hit the bottom of the map, then stop counting
            if (current.Row == _manifold.Length) return 1;
        }

        var left = new Point(current.Row, current.Col - 1);
        var right = new Point(current.Row, current.Col + 1);

        return GetOrAdd(left, GetSplits) + GetOrAdd(right, GetSplits);
    }

    private long GetOrAdd(Point point, Func<Point, long> getValue)
    {
        if (_cache.TryGetValue(point, out var result)) return result;
        result = getValue(point);
        _cache[point] = result;
        return result;
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day07Part1.Example with { ExpectedOutput = "40" }];
}
