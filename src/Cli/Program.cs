// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using AdventOfCode.Cli.Client;
using AdventOfCode.Solutions;
using ConsoleAppFramework;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
builder.AddAdventOfCodeClient();

var app = builder.ToConsoleAppBuilder();
app.Add<Solver>();
await app.RunAsync(args);

class Solver(IAdventOfCodeClient adventOfCodeClient)
{
    private readonly IAdventOfCodeClient _adventOfCodeClient = adventOfCodeClient;

    public async Task Run(int year = 2025, int? day = null, int? part = null, bool examplesOnly = false)
    {
        var query = new SolutionQuery(year);
        if (day is not null) query = query with { Day = day.Value };
        if (part is not null) query = query with { Part = part.Value };

        var matchingSolutions = AllSolutions.Collection.Get(query);

        foreach (var solution in matchingSolutions)
        {
            Console.WriteLine($"\nSolving {solution.DisplayName}");

            foreach (var example in solution.Examples)
            {
                var exampleOutput = solution.Solve(example.Input);
                Console.WriteLine($"> Example Output: {exampleOutput} (Expected: {example.ExpectedOutput})");
            }

            if (!examplesOnly)
            {
                var input = await _adventOfCodeClient.GetInputAsync(solution.Year, solution.Day);
                var answer = solution.Solve(input);
                Console.WriteLine($"> Solution: {answer}");
            }
        }
    }
}
