namespace AdventOfCode.Tests

open System
open FluentAssertions
open Xunit

open AdventOfCode.Solutions

module Tests =
    let testDay fn input expectedOutput =
        fn input |> _.Should().BeEquivalentTo(expectedOutput) |> ignore
        ()

    // [<Theory>]
    // [<InlineData()>]
    // let ``Test day`` day part input expectedOutput =
    //     ()

    [<Fact>]
    let ``My test`` () =
        Assert.True(true)
