module Day5

open System
open AdventOfCode.Common

let parseUpdate (line: string) =
    line.Split(',') |> Array.toList |> List.map int

let parseRule = Tuple2.parse "|" >> Tuple2.map int

let mapFolder map (key, value) =
    match map |> Map.tryFind key with
    | Some list -> map |> Map.add key (value::list)
    | None      -> map |> Map.add key [value]

let buildMap rules = rules |> List.fold mapFolder Map.empty

let parse (input: string array) =
    let splitIndex =
        input
        |> Array.toSeq
        |> Seq.findIndex String.IsNullOrEmpty
    let (rules, updates) =
        input
        |> Array.splitAt splitIndex
        |> Tuple2.map Array.toList
    let forwardRules =
        rules
        |> List.map parseRule
    let backwardRules =
        forwardRules
        |> List.unzip
        |> Tuple2.swap
        ||> List.zip
    (forwardRules |> buildMap,
     backwardRules |> buildMap,
     updates |> List.tail |> List.map parseUpdate)

let sortListByRules fwdRules backRules (l: int list) =
    let comparer a b =
        match fwdRules |> Map.tryFind a with
        | Some list when list |> List.contains b -> -1
        | _ -> match backRules |> Map.tryFind a with
               | Some list when list |> List.contains b -> 1
               | _ -> 0
    l |> List.sortWith comparer

let getMiddleNumber (list: int list) = list |> List.item (list.Length / 2)

let getUpdates success input =
    let (fwdRules, backRules, updates) = parse input
    let sortedUpdates = updates |> List.map (sortListByRules fwdRules backRules)
    let successes =
        (updates, sortedUpdates)
        ||> List.zip
        |> List.map (fun tuple -> tuple ||> List.forall2 (=))
    (match success with | true -> updates | false -> sortedUpdates)
        |> List.zip successes
        |> List.where (fun (s, _) -> s = success)
        |> List.unzip
        |> snd

let solvePart1 = getUpdates true >> List.map getMiddleNumber >> List.sum

let solvePart2 = getUpdates false >> List.map getMiddleNumber >> List.sum
