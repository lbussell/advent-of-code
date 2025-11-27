// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using AdventOfCode.Cli.Client;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddUserSecrets<Program>();
builder.AddAdventOfCodeClient();

var host = builder.Build();

var client = host.Services.GetRequiredService<IAdventOfCodeClient>();
var input = await client.GetInputAsync(2020, 2);
Console.WriteLine(input);
