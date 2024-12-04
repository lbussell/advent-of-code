namespace AdventOfCode.Tests

open System.IO
open FluentAssertions
open Xunit

open AdventOfCode.Solutions

module Tests =

    [<Theory>]
    [<InlineData(1, 0, "1a.txt", 11)>]
    [<InlineData(1, 1, "1a.txt", 31)>]
    [<InlineData(2, 0, "2a.txt", 2)>]
    [<InlineData(2, 1, "2a.txt", 4)>]
    [<InlineData(2, 1, "2b.txt", 10)>]
    [<InlineData(2, 1, "2c.txt", 0)>]
    [<InlineData(2, 1, "2d.txt", 1)>]
    let ``Test day`` day part inputFile expectedOutput =
        let input = inputFile |> File.ReadAllLines
        let solver = Solutions.getSolver day part
        let result = solver input
        result.Should().Be(expectedOutput)
