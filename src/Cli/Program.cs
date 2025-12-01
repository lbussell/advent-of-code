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

rootCommand.SetAction(async parseResult =>
{
    var query = new SolutionQuery();

    var year = parseResult.GetValue(yearOption) ?? -1;
    if (year != -1)
    {
        query = query with { Year = year };
    }

    var day = parseResult.GetValue(dayOption) ?? -1;
    if (day != -1)
    {
        query = query with { Day = day };
    }

    var part = parseResult.GetValue(partOption) ?? -1;
    if (part != -1)
    {
        query = query with { Part = part };
    }

    var solutions = AllSolutions.Collection;
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

        var input = await adventOfCodeClient.GetInputAsync(solution.Year, solution.Day);
        var answer = solution.Solve(input);
        Console.WriteLine($"> Solution: {answer}");
    }
    Console.WriteLine();
});

var parseResult = rootCommand.Parse(args);
await parseResult.InvokeAsync();
