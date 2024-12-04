module Day2

open AdventOfCode.Common

let parse (line: string) =
    line
    |> _.Split(' ')
    |> Seq.toList
    |> List.map int

let diff (l: int list) =
    l
    |> List.pairwise
    |> List.map Tuple2.diff

let processReport report =
    let diff = report |> diff
    let a = diff |> Seq.forall (fun d -> d > 0)
    let b = diff |> Seq.forall (fun d -> d < 0)
    let c = diff |> Seq.forall (fun d -> abs d >= 1 && abs d <= 3)
    (a || b) && c

let solvePart1 (input: string array) =
    input
    |> Seq.toList
    |> List.map parse
    |> List.map processReport
    |> List.where id
    |> _.Length

let generateNewReports (report: int list): int list list =
    (report |> List.mapi (fun i _ -> report |> List.removeAt i)) @ [report]

let processMultipleReports (reports: int list list) =
    reports
    |> List.map processReport
    |> Seq.exists id

let solvePart2 (input: string array) =
    input
    |> Seq.toList
    |> List.map parse
    |> List.map generateNewReports
    |> List.map processMultipleReports
    |> List.where id
    |> _.Length
