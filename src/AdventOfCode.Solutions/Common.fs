namespace AdventOfCode.Common

open System.Diagnostics

module Diagnostics =
    let time (f: unit -> int) =
        let stopwatch = Stopwatch.StartNew()
        let result = f ()
        stopwatch.Stop()
        (result, stopwatch.Elapsed)


module Tuple2 =

    let diff (a, b) = b - a

    let diff2 (a, b) = a - b

    let map f (a, b) = (f a, f b)

    let parse (d: string) (s: string) =
        match s |> _.Split(d) with
        | [| a; b |] -> (a, b)
        | _ -> failwith $"Could not parse tuple from `{s}` and delimiter `{d}`"
