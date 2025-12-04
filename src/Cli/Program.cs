// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AdventOfCode.Cli.Client;
using AdventOfCode.Solutions;


var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddUserSecrets<Program>();
builder.AddAdventOfCodeClient();

var host = builder.Build();
await host.StartAsync();

var rootCommand = new RootCommand();

var yearOption = new Option<int?>("--year");
rootCommand.Options.Add(yearOption);

var dayOption = new Option<int?>("--day");
rootCommand.Options.Add(dayOption);

var partOption = new Option<int?>("--part");
rootCommand.Options.Add(partOption);

var examplesOnlyOption = new Option<bool>("--examples-only");
rootCommand.Options.Add(examplesOnlyOption);

rootCommand.SetAction(async parseResult =>
{
    var solutions = AllSolutions.Collection;
    var query = new SolutionQuery();

    var year = parseResult.GetValue(yearOption);
    query = year switch
    {
        int y => query with { Year = y },
        _     => query with { Year = solutions.All.Max(s => s.Year) },
    };

    var day = parseResult.GetValue(dayOption);
    if (day is not null) query = query with { Day = day.Value };

    var part = parseResult.GetValue(partOption);
    if (part is not null) query = query with { Part = part.Value };

    var matchingSolutions = solutions.Get(query);

    var adventOfCodeClient = host.Services.GetRequiredService<IAdventOfCodeClient>();

    foreach (var solution in matchingSolutions)
    {
        Console.WriteLine($"\nSolving {solution.DisplayName}");

        foreach (var example in solution.Examples)
        {
            var exampleOutput = solution.Solve(example.Input);
            Console.WriteLine($"> Example Output: {exampleOutput} (Expected: {example.ExpectedOutput})");
        }

        if (!parseResult.GetValue(examplesOnlyOption))
        {
            var input = await adventOfCodeClient.GetInputAsync(solution.Year, solution.Day);
            var answer = solution.Solve(input);
            Console.WriteLine($"> Solution: {answer}");
        }
    }
    Console.WriteLine();
});

var parseResult = rootCommand.Parse(args);
await parseResult.InvokeAsync();
