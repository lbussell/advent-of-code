// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;

namespace AdventOfCode.Cli.Client;

internal interface IAdventOfCodeClient
{
    Task<string> GetInputAsync(int year, int day);
}

internal sealed class AdventOfCodeClient(HttpClient httpClient) : IAdventOfCodeClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<string> GetInputAsync(int year, int day)
    {
        var response = await _httpClient.GetAsync($"/{year}/day/{day}/input");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
