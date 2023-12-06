namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day5(IConfig config) : SolutionWithTextInput(config)
{
    public override int Day => 5;

    public override string Name => "If You Give A Seed A Fertilizer";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        IEnumerable<long> seeds = ParseSeeds(Input.First());

        // Avoid multiple enumeration
        var mapGroups = ParseRanges(Input.Skip(1).Where(s => !string.IsNullOrEmpty(s))).ToArray();

        return seeds
            .Select(seed => Trace(
                seed,
                mapGroups,
                (mapping, l) => mapping.CheckSrc(l),
                (m, l) => m.SrcToDest(l)))
            .Min()
            .ToString();
    }

    private string SolvePart2()
    {
        IEnumerable<LongRange> seeds = ParseSeeds2(Input.First());

        // Run it in reverse
        var mapGroups = ParseRanges(Input.Skip(1).Where(s => !string.IsNullOrEmpty(s)))
            .Reverse()
            .ToArray();

        // Upper bound for the search is the highest location.
        long upperBound = mapGroups.First().Select(m => m.Dest + m.Len).Max();

        var runSearch = (long start, int interval) =>
        {
            for (long location = start; location <= upperBound; location += interval)
            {
                long potentialSeed = Trace(
                    location,
                    mapGroups,
                    (map, l) => map.CheckDest(l),
                    (map, l) => map.DestToSrc(l));

                var matchedSeeds = seeds.Where(seedRange => seedRange.Check(potentialSeed));

                if (matchedSeeds.Any())
                {
                    return location;
                }
            }

            return -1;
        };

        long result = -1;
        long start = 0;
        int interval = 1000;
        while (interval >= 1)
        {
            result = runSearch(start, interval);
            start = result - (interval * interval);
            interval /= 10;
        }

        return result.ToString();
    }

    private static long Trace(
        long start,
        IEnumerable<IEnumerable<Mapping>> mapGroups,
        Func<Mapping, long, bool> filter,
        Func<Mapping, long, long> getNext)
    {
        long current = start;
        foreach (var mapGroup in mapGroups)
        {
            Mapping m = mapGroup
                .Where(m2 => filter(m2, current))
                .FirstOrDefault(Mapping.DefaultMapping);
            current = getNext(m, current);
        }

        return current;
    }

    private static long TraceBackwards(long location, IEnumerable<IEnumerable<Mapping>> mapGroups)
    {
        long currentLocation = location;

        // Assume mapGroups is already reversed
        // Skip the first group, because that's where we got the location
        foreach (var mapGroup in mapGroups.Skip(1))
        {
            Mapping m = mapGroup
                .Where(m2 => m2.CheckDest(currentLocation))
                .FirstOrDefault(Mapping.DefaultMapping);
            currentLocation = m.DestToSrc(currentLocation);
        }

        return currentLocation;
    }

    // Example input:
    // seeds: 79 14 55 13
    private static IEnumerable<long> ParseSeeds(string inp)
        => inp.Split(':', Util.SSOpts)
            .ToArray()[1]
            .Split(' ', Util.SSOpts)
            .Select(long.Parse);

    private static IEnumerable<LongRange> ParseSeeds2(string inp)
        => inp.Split(':', Util.SSOpts)
            .ToArray()[1]
            .Split(' ', Util.SSOpts)
            .Chunk(2)
            .Select(s => s.Select(long.Parse).ToArray())
            .Select(lr => new LongRange(lr[0], lr[0] + lr[1]));

    // Example input for one mapping:
    // DEST SRC LEN
    // seed-to-soil map:
    // 50 98 2
    // 52 50 48
    private static IEnumerable<IEnumerable<Mapping>> ParseRanges(IEnumerable<string> inp)
    {
        while (inp.Any())
        {
            // Don't need the header
            // string header = inp.First();
            inp = inp.Skip(1);

            int toSkip = 0;
            foreach (string line in inp)
            {
                if (!char.IsDigit(line[0]))
                {
                    break;
                }

                toSkip += 1;
            }

            IEnumerable<string> outp = inp.Take(toSkip);
            inp = inp.Skip(toSkip);
            yield return outp.Select(ParseMapping);
        }
    }

    private static Mapping ParseMapping(string inp)
    {
        var t = inp.Split(' ').Select(long.Parse).ToArray();
        return new Mapping(t[0], t[1], t[2]);
    }

    private record Mapping(long Dest, long Src, long Len)
    {
        public static Mapping DefaultMapping => new(0, 0, long.MaxValue);

        public long DestToSrc(long n) => n - Dest + Src;

        public long SrcToDest(long n) => n - Src + Dest;

        public bool CheckDest(long n) => Dest <= n && n < Dest + Len;

        public bool CheckSrc(long n) => Src <= n && n < Src + Len;
    }

    private record LongRange(long Start, long End)
    {
        public bool Check(long n) => Start <= n && n < End;
    }
}
