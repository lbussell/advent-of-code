namespace AdventOfCode.Solutions2023;

using AdventOfCode.Core;

public sealed class Day06 : ISolution
{
    public int Year => 2023;

    public int Day => 6;

    public string Name => "Wait For It";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private static string SolvePart1(IEnumerable<string> input)
    {
        IEnumerable<int> times = Parse(input.First());
        IEnumerable<int> distances = Parse(input.Skip(1).First());

        IEnumerable<BoatRace> boatRaces = times.Zip(distances)
            .Select(td => new BoatRace(td.First, td.Second));

        return boatRaces
            .Select(br => br.NumberOfWinConditions)
            .Product()
            .ToString();
    }

    private static string SolvePart2(IEnumerable<string> input)
    {
        long t = Parse2(input.First());
        long d = Parse2(input.Skip(1).First());
        long result = 0;
        for (long ht = 1; ht < t; ht += 1)
        {
            if (ht * (t - ht) > d)
            {
                result += 1;
            }
        }

        return result.ToString();
    }

    private static IEnumerable<int> Parse(string input)
        => input.Split(':', Constants.SSOpts)
            .ToArray()[1]
            .Split(' ', Constants.SSOpts)
            .Select(int.Parse);

    private static long Parse2(string input)
        => long.Parse(input.Split(':', Constants.SSOpts)
            .ToArray()[1]
            .Split(' ', Constants.SSOpts)
            .Aggregate(string.Concat));
}

internal sealed record BoatRace
{
    public BoatRace(int t, int d)
    {
        Time = t;
        DistanceRecord = d;
    }

    public int Time { get; init; }

    public int DistanceRecord { get; init; }

    public int NumberOfWinConditions
    {
        get
        {
            int w = 0;
            foreach (int holdTime in Enumerable.Range(1, Time))
            {
                if (RunRace(holdTime))
                {
                    w += 1;
                }
            }

            return w;
        }
    }

    public bool RunRace(int holdTimeInMs)
    {
        int v = holdTimeInMs;
        int t = Time - holdTimeInMs;
        return v * t > DistanceRecord;
    }
}
