namespace AdventOfCode.Tests

open System
open System.IO
open FluentAssertions
open Xunit

open AdventOfCode.Solutions

module Tests =

    [<Theory>]
    [<InlineData(1, 0, "1a.txt", 11)>]
    [<InlineData(1, 1, "1a.txt", 31)>]
    [<InlineData(2, 0, "1a.txt", 2)>]
    [<InlineData(2, 1, "1a.txt", 0)>]
    let ``Test day`` day part inputFile expectedOutput =
        let input = inputFile |> File.ReadAllLines
        let solver = Solutions.getSolver day part
        let result = solver input
        result.Should().Be(expectedOutput)

    // [<Fact>]
    // let ``My test`` () =
    //     Assert.True(true)
