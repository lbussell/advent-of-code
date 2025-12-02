// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Diagnostics;

namespace AdventOfCode.Solutions.Day02;

/// <param name="Start">Inclusive</param>
/// <param name="End">Inclusive</param>
internal readonly record struct IDRange(long Start, long End)
{
    /// <param name="input">The range as a string, in the format "11-22"</param>
    public static implicit operator IDRange(string input)
    {
        var parts = input.Split('-');
        Debug.Assert(parts.Length == 2);
        return new IDRange(long.Parse(parts[0]), long.Parse(parts[1]));
    }

    public IEnumerable<long> ToEnumerable()
    {
        for (var i = Start; i <= End; i++) yield return i;
    }
}

internal sealed class Day02Part1() : Solution(2025, 2, 1)
{
    public override string Solve(string input)
    {
        var invalidIds = input
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => (IDRange)s)
            .SelectMany(idRange => idRange.ToEnumerable())
            .Where(id => !IsValid(id));

        return invalidIds.Sum().ToString();
    }

    /// <summary>
    /// Valid digits are made only of some sequence of digits repeated twice.
    /// So, 55 (5 twice), 6464 (64 twice), and 123123 (123 twice) would all be invalid IDs.
    /// </summary>
    private static bool IsValid(long id)
    {
        var idString = id.ToString();
        var length = idString.Length;

        // All odd length numbers are valid
        if (length % 2 != 0) return true;

        var halfLength = length / 2;
        var firstHalf = idString[..halfLength];
        var secondHalf = idString[halfLength..];

        // If both halves are not the same, it's valid
        return firstHalf != secondHalf;
    }

    public override IEnumerable<Example> Examples { get; } = [
        new Example(
            Input: "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,"
                + "1698522-1698528,446443-446449,38593856-38593862,565653-565659,"
                + "824824821-824824827,2121212118-2121212124",
            ExpectedOutput: "1227775554"
        )
    ];
}

internal sealed class Day02Part2() : Solution(2025, 2, 2)
{
    public override string Solve(string input)
    {
        var invalidIds = input
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => (IDRange)s)
            .SelectMany(idRange => idRange.ToEnumerable())
            .Where(id => !IsValid(id));

        return invalidIds.Sum().ToString();
    }

    private static bool IsValid(long id)
    {
        var idString = id.ToString();
        var length = idString.Length;

        var maxRepeats = length;
        for (int i = 2; i <= maxRepeats; i += 1)
        {
            if (CheckForRepeats(idString, i))
            {
                return false;
            }
        }

        return true;
    }

    private static bool CheckForRepeats(string input, int segments)
    {
        if (input.Length % segments != 0)
        {
            return false;
        }

        var segmentLength = input.Length / segments;
        var segment = input[..segmentLength];

        for (int i = 1; i < segments; i++)
        {
            var nextSegment = input.Substring(i * segmentLength, segmentLength);
            if (segment != nextSegment)
            {
                return false;
            }
        }

        return true;
    }

    public override IEnumerable<Example> Examples { get; } = [
        new Example(
            Input: "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,"
                + "1698522-1698528,446443-446449,38593856-38593862,565653-565659,"
                + "824824821-824824827,2121212118-2121212124",
            ExpectedOutput: "4174379265"
        )
    ];
}
