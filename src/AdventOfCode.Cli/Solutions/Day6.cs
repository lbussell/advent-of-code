namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day6(IConfig config) : SolutionWithTextInput(config)
{
    public override int Day => 6;

    public override string Name => "Wait For It";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        IEnumerable<int> times = Parse(Input.First());
        IEnumerable<int> distances = Parse(Input.Skip(1).First());

        IEnumerable<BoatRace> boatRaces = times.Zip(distances)
            .Select(td => new BoatRace(td.First, td.Second));

        return boatRaces
            .Select(br => br.NumberOfWinConditions)
            .Product()
            .ToString();
    }

    private string SolvePart2()
    {
        long t = Parse2(Input.First());
        long d = Parse2(Input.Skip(1).First());
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
        => input.Split(':', Util.SSOpts)
            .ToArray()[1]
            .Split(' ', Util.SSOpts)
            .Select(int.Parse);

    private static long Parse2(string input)
        => long.Parse(input.Split(':', Util.SSOpts)
            .ToArray()[1]
            .Split(' ', Util.SSOpts)
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
