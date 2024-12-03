namespace AdventOfCode.Common

module Tuple2 =

    let map f (a, b) = (f a, f b)

    let parse (d: string) (s: string) =
        match s |> _.Split(d) with
        | [| a; b |] -> (a, b)
        | _ -> failwith $"Could not parse tuple from `{s}` and delimiter `{d}`"
