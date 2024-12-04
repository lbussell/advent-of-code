namespace AdventOfCode.Solutions

module Solutions =
    let all: Map<int, (string array -> int) list> = Map [
        0, [DayTemplate.solvePart1; DayTemplate.solvePart2];
        1, [Day1.solvePart1; Day1.solvePart2];
        2, [Day2.solvePart1; Day2.solvePart2];
        3, [Day3.solvePart1; Day3.solvePart2];
    ]

    let getSolvers day =
        Map.find day all

    let getSolver day part =
        day
        |> getSolvers
        |> _.Item(part)
