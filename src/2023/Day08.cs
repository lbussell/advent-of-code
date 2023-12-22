namespace AdventOfCode.Solutions2023;

using System.Text.RegularExpressions;
using AdventOfCode.Core;

using Graph = Dictionary<string, (string L, string R)>;
using Node = (string L, string R);

public sealed class Day08() : ISolution
{
    private Graph _graph = [];

    private bool[] _sequence = [];

    public int Year => 2023;

    public int Day => 8;

    public string Name => "Haunted Wasteland";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1(IEnumerable<string> input)
    {
        // L = false, R = right
        _sequence = GetSequence(input.First());
        _graph = GetGraph(input.Skip(2));

        string current = "AAA";
        int sequencePtr = 0;
        int steps = 0;
        while (current != "ZZZ")
        {
            if (!_graph.TryGetValue(current, out Node n))
            {
                throw new Exception();
            }

            current = _sequence[sequencePtr] ? n.R : n.L;
            sequencePtr = (sequencePtr + 1) % _sequence.Length;
            steps += 1;
        }

        return steps.ToString();
    }

    private static Graph GetGraph(IEnumerable<string> input)
    {
        Graph nodes = [];

        string pattern = @"(\w+) = \((\w+), (\w+)\)";
        foreach (string line in input)
        {
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                string src = match.Groups[1].Value;
                string l = match.Groups[2].Value;
                string r = match.Groups[3].Value;
                nodes.Add(src, (l, r));
            }
        }

        return nodes;
    }

    private static bool[] GetSequence(string input)
        => input.ToCharArray().Select(c => c == 'R').ToArray();

    private string SolvePart2(IEnumerable<string> input)
    {
        _sequence = GetSequence(input.First());
        _graph = GetGraph(input.Skip(2));

        // Make some silly assumptions I read on reddit after I got stuck
        // - The answer is the LCM of each cycle, and the cycle length is
        //   equal to the number of steps from each A to the first Z.
        return _graph.Keys
            .Where(s => s[2] == 'A')
            .Select(StepsToZ)
            .LCM()
            .ToString();
    }

    private long StepsToZ(string s)
    {
        long steps = 0;
        long sequencePtr = 0;
        string current = s;
        while (current[2] != 'Z')
        {
            _graph.TryGetValue(current, out Node node);
            current = _sequence[sequencePtr] ? node.R : node.L;
            sequencePtr = (sequencePtr + 1) % _sequence.Length;
            steps += 1;
        }

        long cycleLength = steps;
        Console.WriteLine(cycleLength);
        return cycleLength;
    }
}
