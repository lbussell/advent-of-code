namespace AdventOfCode

open System
open System.IO

open AdventOfCode.Common
open AdventOfCode.Solutions

module Program =

    let printUsage () =
        """

        Missing required arguments. Usage:
        dotnet run -- <day>
        dotnet run -- all

        """ |> Console.WriteLine

    let getInput day =
        let fileName = day.ToString() + ".txt"
        fileName |> File.ReadAllLines

    let runWithLogging day part input =
        let solver = Solutions.getSolver day part

        let log result time =
            printfn "Day %d Part %d: %d (%A)" day (part + 1) result time

        (fun () -> solver input)
            |> Diagnostics.time
            ||> log

    [<EntryPoint>]
    let main args =
        let runDay day =
            let input = day |> getInput
            for part in 0..1 do runWithLogging day part input

        let runAll () =
            Solutions.all.Keys
            |> Seq.toList
            |> List.map runDay

        match args with
        | [| "all" |] -> runAll ()
        | [| day |] -> [day |> int |> runDay]
        | _ -> [printUsage ()]
        |> ignore

        0
