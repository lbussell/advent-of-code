// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Collections;
using System.Text;

namespace AdventOfCode.Solutions.Day04;

internal interface IGridConverter<T>
{
    T ToItem(char c);
    char ToChar(T item);
}

/// <summary>
/// Represents world based around a grid of T.
/// </summary>
internal sealed record GridWorld<T> : IEnumerable<GridPosition<T>>
{
    private readonly T[][] _world;
    private readonly IGridConverter<T> _converter;

    public GridWorld(string input, IGridConverter<T> converter)
    {
        _converter = converter;
        _world = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                      .Select(line => line.Select(_converter.ToItem).ToArray())
                      .ToArray();
    }

    public int Rows => _world.Length;
    public int Columns => _world[0].Length;

    public GridPosition<T> Get(int r, int c) => new(this, _world[r][c], r, c);

    public void Set(int r, int c, T value) => _world[r][c] = value;

    public override string ToString()
    {
        var output = new StringBuilder();

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                output.Append(_converter.ToChar(_world[r][c]));
            }

            output.AppendLine();
        }

        return output.ToString();
    }

    public IEnumerator<GridPosition<T>> GetEnumerator()
    {
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
                yield return new GridPosition<T>(this, _world[r][c], r, c);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal readonly record struct GridPosition<T>(GridWorld<T> World, T Value, int Row, int Column);

internal static class GridExtensions
{
    extension<T>(GridPosition<T> position)
    {
        // ***
        // *@*
        // ***
        public IEnumerable<GridPosition<T>> Adjacent()
        {
            var maxLeft = position.Column <= 0;
            var maxRight = position.Column >= position.World.Columns - 1;
            var maxTop = position.Row <= 0;
            var maxBottom = position.Row >= position.World.Rows - 1;

            if (!maxTop)
            {
                yield return position.World.Get(position.Row - 1, position.Column); // top
                if (!maxLeft) yield return position.World.Get(position.Row - 1, position.Column - 1); // top-left
                if (!maxRight) yield return position.World.Get(position.Row - 1, position.Column + 1); // top-right
            }

            if (!maxLeft) yield return position.World.Get(position.Row, position.Column - 1); // left
            if (!maxRight) yield return position.World.Get(position.Row, position.Column + 1); // right

            if (!maxBottom)
            {
                yield return position.World.Get(position.Row + 1, position.Column); // bottom
                if (!maxLeft) yield return position.World.Get(position.Row + 1, position.Column - 1); // bottom-left
                if (!maxRight) yield return position.World.Get(position.Row + 1, position.Column + 1); // bottom-right
            }
        }
    }
}

internal enum GridItem
{
    Empty,
    Paper,
}

internal sealed class Day04Converter() : IGridConverter<GridItem>
{
    public GridItem ToItem(char c) => c switch
    {
        '@' => GridItem.Paper,
         _  => GridItem.Empty,
    };

    public char ToChar(GridItem item) => item switch
    {
        GridItem.Paper => '@',
        _              => '.',
    };
}

internal sealed class Day04Part1() : Solution(2025, 4, 1)
{
    public override string Solve(string input)
    {
        var world = new GridWorld<GridItem>(input, new Day04Converter());

        return world.Where(position => position.Value is GridItem.Paper)
                    .Where(position => position.Adjacent()
                                               .Where(adjacent => adjacent.Value is GridItem.Paper)
                                               .Count() < 4)
                    .Count()
                    .ToString();
    }

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        ..@@.@@@@.
        @@@.@.@.@@
        @@@@@.@.@@
        @.@@@@..@.
        @@.@@@@.@@
        .@@@@@@@.@
        .@.@.@.@@@
        @.@@@.@@@@
        .@@@@@@@@.
        @.@.@@@.@.
        """,
        "13");
}

internal sealed class Day04Part2() : Solution(2025, 4, 2)
{
    public override string Solve(string input)
    {
        var world = new GridWorld<GridItem>(input, new Day04Converter());
        var paperRemoved = 0;

        while (HasRemovablePaper(world))
        {
            foreach (var position in GetRemovablePaper(world))
            {
                world.Set(position.Row, position.Column, GridItem.Empty);
                paperRemoved += 1;
            }
        }

        return paperRemoved.ToString();
    }

    private static IEnumerable<GridPosition<GridItem>> GetRemovablePaper(GridWorld<GridItem> world) =>
        world.Where(position => position.Value is GridItem.Paper)
             .Where(position => position.Adjacent()
                                        .Where(adjacent => adjacent.Value is GridItem.Paper)
                                        .Count() < 4);

    private static bool HasRemovablePaper(GridWorld<GridItem> world) => GetRemovablePaper(world).Any();

    public override IEnumerable<Example> Examples { get; } = [Day04Part1.Example with { ExpectedOutput = "43" }];
}
