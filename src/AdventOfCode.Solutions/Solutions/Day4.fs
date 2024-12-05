module Day4

open System.Text.RegularExpressions

let reg = Regex("XMAS|SAMX")
let sep = "-"
let _or = "|"
let positiveLookahead s = $"(?={s})"

let nextLine n = sprintf ".{%d}" (n)
let down len = nextLine len
let diagRight len = nextLine (len+1)
let diagLeft len = nextLine (len-1)
let oneLine s = s |> (String.concat "-")

let solvePart1 (input: string array) =
    let len = input[0].Length
    // orientations
    let across = ""
    let down = down len
    let diagRight = diagRight len
    let diagLeft = diagLeft len
    // directions
    let forwards = ["X";"M";"A";"S"] |> List.toSeq
    let backwards = forwards |> Seq.rev
    let patterns = [
        for orientation in [across; down; diagRight; diagLeft] do
        for direction in [forwards; backwards] do
            yield (orientation, direction) ]
                |> List.map (fun t -> t ||> String.concat)
                |> List.map positiveLookahead

    patterns
    |> List.map (fun p -> Regex(p))
    |> List.map (fun r -> r.Matches(input |> oneLine))
    |> List.sumBy (fun matches -> matches.Count)

let solvePart2 (input: string array) =
    let len = input[0].Length
    let nextLine = sprintf ".{%d}" (len-1)
    let pattern = positiveLookahead <| String.concat "" [ 
            "M.S"; nextLine;
            "A";   nextLine;
            "M.S";
            _or;
            "M.M"; nextLine;
            "A";   nextLine;
            "S.S";
            _or;
            "S.M"; nextLine;
            "A";   nextLine;
            "S.M";
            _or;
            "S.S"; nextLine;
            "A";   nextLine;
            "M.M";
        ]
    let regex = Regex <| pattern
    regex.Matches(input |> oneLine).Count
