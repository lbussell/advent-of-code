module Day1

open AdventOfCode.Common

let split (line: string) = line.Split "   "

let toTuple (line: string array) =
    match line with
    | [| a; b |] -> (a, b)
    | _ -> failwith $"Input was not as expected: {line}"

let stringListToIntList (l: string list) =
    l |> List.map (fun s -> s |> int)

let difference (a, b) = (a - b) |> abs

let solvePart1 (input: string array) =
    input
    |> Seq.toList
    |> List.map split
    |> List.map toTuple
    |> List.unzip
    |> Tuple2.map stringListToIntList
    |> Tuple2.map (List.sortBy abs)
    ||> List.zip
    |> List.map difference
    |> List.sum

let solvePart2 (input: string array) =
    let list1, list2 =
        input
        |> Seq.toList
        |> List.map split
        |> List.map toTuple
        |> List.unzip
        |> Tuple2.map stringListToIntList

    let occurrences =
        list2
        |> Seq.countBy id
        |> dict

    let similarityScore n: int =
        try
            n * (occurrences.Item n)
        with
            | _ -> 0

    list1 |> Seq.sumBy similarityScore
