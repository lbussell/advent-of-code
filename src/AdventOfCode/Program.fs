namespace AdventOfCode

open System
open AdventOfCode.Solutions

module Program =
    let runOne day: string =
        let solvers = Map.find day Solutions.all
        let input = Solutions.loadInput day
        let outputs = solvers |> List.map (fun solver -> solver input)
        let outputsString = String.Join(", ", (outputs |> List.map string))
        $"Day {day} solutions: {outputsString}"

    let runAll (solutions: Map<int, (string array -> int) list>): string list =
        solutions.Keys
        |> Seq.toList
        |> List.map runOne

    [<EntryPoint>]
    let main args =
        printfn $"Arguments passed to function : %A{args}"

        let answers =
            match args with
            | [| "all" |] -> runAll Solutions.all
            | [| s |] -> [s |> int |> runOne]
            | _ -> failwith "Unexpected set of arguments"

        answers |> List.map Console.WriteLine |> ignore

        0
