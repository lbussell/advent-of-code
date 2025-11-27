// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Options;

namespace AdventOfCode.Cli.Client;

internal sealed class InputCache(IOptions<AdventOfCodeOptions> config)
{
    private readonly string _cacheDirectory = config.Value.CacheDirectory;

    public void Write(int year, int day, string contents)
    {
        var filePath = GetFilePath(year, day);
        File.WriteAllText(filePath, contents);
    }

    public string? Read(int year, int day)
    {
        var filePath = GetFilePath(year, day);
        return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
    }

    private string GetFilePath(int year, int day)
    {
        var fullCacheDirectory = Path.GetFullPath(_cacheDirectory);

        if (!Directory.Exists(fullCacheDirectory))
        {
            Directory.CreateDirectory(fullCacheDirectory);
        }

        return Path.Combine(fullCacheDirectory, GetFileName(year, day));
    }

    private static string GetFileName(int year, int day) => $"{year}-day{day}.txt";
}
