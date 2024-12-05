module Day3

open System.Text.RegularExpressions
open AdventOfCode.Common

let mul x y = x * y

let dontRegex = Regex("^don't\(\)")
let doRegex = Regex("^do\(\)")
let mulRegex = Regex("^mul\((\d{1,3}),(\d{1,3})\)")

let handleMatch2 (m: Match) (accum: int) (enabled: bool) (input: string) =
    let doMultiplication (m: Match) =
        [ for group in m.Groups -> group.Value ]
        |> List.tail
        |> List.map int
        |> Tuple2.parseIntList
        ||> mul
    let length =
        m.Groups[0].Value
        |> String.length
    let remainder = input.Remove(0, length)
    match enabled with
    | true -> (accum + (doMultiplication m), enabled, remainder)
    | false -> (accum, enabled, remainder)

let rec mullItOver (accum: int) (enabled: bool) (input: string) =
    let doRecursion () =
        let m = mulRegex.Match(input)
        match m.Success with
        | true -> input |> (handleMatch2 m accum enabled)
        | false -> (accum, enabled, input.Remove(0,1))
    match input |> String.length with
    | 0 -> (accum, enabled, input)
    | _ -> doRecursion () |||> mullItOver

let solvePart1 (input: string array) =
    let (accum, _, _) =
        input
        |> String.concat ""
        |> (mullItOver 0 true)
    accum

let rec mullItOver2 (accum: int) (enabled: bool) (input: string) =
    let doRecursion () =
        let enable = doRegex.Match(input)
        let disable = dontRegex.Match(input)
        let mul = mulRegex.Match(input)
        match (enable.Success, disable.Success, mul.Success) with
        | (false, false, false) -> (accum, enabled, input.Remove(0,1))
        | (true,  _,     _    ) -> (accum, true, input.Remove(0,1))
        | (_,     true,  _    ) -> (accum, false, input.Remove(0,1))
        | (_,     _,     true ) -> (accum, enabled, input) |||> (handleMatch2 mul)
    match input |> String.length with
    | 0 -> (accum, enabled, input)
    | _ -> doRecursion () |||> mullItOver2

let solvePart2 (input: string array) = 
    let (accum, _, _) =
        input
        |> String.concat ""
        |> (mullItOver2 0 true)
    accum
