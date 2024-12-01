namespace AdventOfCode

open System
open System.IO

module Program =
    let allSolutions: Map<int, (string array -> int) list> = Map [
        1, [Day1.solvePart1; Day1.solvePart2];
    ]

    let runOne day: string =
        let solvers = (Map.find day allSolutions)
        let input: string array = File.ReadAllLines $"./Inputs/{day}.txt"
        let outputs: int list = solvers |> List.map (fun solver -> solver input)
        let outputsString = String.Join(", ", (outputs |> List.map string))
        $"Day {day} solutions: {outputsString}"

    let runAll (solutions: Map<int, (string array -> int) list>): string list =
        solutions.Keys |> Seq.toList |> List.map runOne

    [<EntryPoint>]
    let main args =
        printfn $"Arguments passed to function : %A{args}"

        let answers =
            match args with
            | [| "all" |] -> runAll allSolutions
            | [| s |] -> [s |> int |> runOne]
            | _ -> failwith "Unexpected set of arguments"

        answers |> List.map Console.WriteLine |> ignore

        0
