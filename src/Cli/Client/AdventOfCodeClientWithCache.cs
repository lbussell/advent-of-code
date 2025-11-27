// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Cli.Client;

internal sealed class AdventOfCodeClientWithCache(AdventOfCodeClient client, InputCache cache) : IAdventOfCodeClient
{
    private readonly IAdventOfCodeClient _client = client;
    private readonly InputCache _cache = cache;

    public async Task<string> GetInputAsync(int year, int day)
    {
        string? cachedInput = _cache.Read(year, day);
        if (cachedInput is not null)
        {
            return cachedInput;
        }

        string input = await _client.GetInputAsync(year, day);
        _cache.Write(year, day, input);
        return input;
    }
}
