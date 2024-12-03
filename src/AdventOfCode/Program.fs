namespace AdventOfCode

open System
open System.IO
open System.Diagnostics

open AdventOfCode.Common
open AdventOfCode.Solutions

module Program =

    let loadEnv path =
        path
        |> File.ReadAllLines
        |> Seq.toList
        |> List.map (Tuple2.parse "=")
        |> dict

    [<EntryPoint>]
    let main args =
        let printUsage = 
            "Missing some required arguments" |> Console.WriteLine
            "Usage: dotnet run -- /path/to/inputs <1|2|3|...|all>" |> Console.WriteLine
            0

        let time (f: unit -> int) =
            let stopwatch = Stopwatch.StartNew()
            let result = f ()
            stopwatch.Stop()
            (result, stopwatch.Elapsed)

        let format day part result t =
            $"Day {day} Part {part + 1} = {result} ({t})" 

        let runPart day part solver input =
            let f = fun () -> solver input 
            let result = f |> time
            result ||> (format day part) |> Console.WriteLine
            result

        let runDay inputsDir day =
            let solvers = Map.find day Solutions.all
            let input = Path.Combine(inputsDir, $"{day}.txt") |> File.ReadAllLines
            solvers |> List.mapi (fun i solver -> runPart day i solver input)

        let runDays inputsDir days =
            days |> List.map (fun day -> runDay inputsDir day)

        let getDays daysArg =
            match daysArg with
            | "all" -> Solutions.all.Keys |> Seq.toList
            | n -> [int n]

        let run (args: string array) =
            match args with
            | [| inputsDir; daysArg |] -> runDays inputsDir (getDays daysArg) |> ignore
            | _ -> printUsage |> ignore

        args |> run

        0
