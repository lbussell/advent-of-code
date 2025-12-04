// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using AdventOfCode.Solutions.Grid;

namespace AdventOfCode.Solutions.Day04;

internal sealed class Day04Part1() : Solution(2025, 4, 1)
{
    public override string Solve(string input)
    {
        var world = new GridWorld<GridItem>(input, new Day04Converter());

        return world.Where(position => position.Value is GridItem.Paper)
                    .Where(position =>
                           position.Adjacent()
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

internal enum GridItem
{
    Empty,
    Paper,
}
