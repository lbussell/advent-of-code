namespace AdventOfCode.Solutions

open System.IO

module Solutions =
    let all: Map<int, (string array -> int) list> = Map [
        1, [Day1.solvePart1; Day1.solvePart2];
    ]

    let getInputFileName day = $"./Inputs/{day}.txt"

    let loadInput day =
        day |> getInputFileName |> File.ReadAllLines
