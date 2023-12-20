namespace AdventOfCode.Solutions2023;

using System.Text.RegularExpressions;
using AdventOfCode.Core;

using Graph = Dictionary<string, (string L, string R)>;
using Node = (string L, string R);

public sealed class Day08() : ISolution
{
    public int Year => 2023;

    public int Day => 8;

    public string Name => "Haunted Wasteland";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private static string SolvePart1(IEnumerable<string> input)
    {
        // L = false, R = right
        bool[] sequence = GetSequence(input.First());
        Graph graph = GetGraph(input.Skip(2));

        string current = "AAA";
        int sequencePtr = 0;
        int steps = 0;
        while (current != "ZZZ")
        {
            if (!graph.TryGetValue(current, out Node n))
            {
                throw new Exception();
            }

            current = sequence[sequencePtr] ? n.R : n.L;
            sequencePtr = (sequencePtr + 1) % sequence.Length;
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

    private static string SolvePart2(IEnumerable<string> input) => throw new NotImplementedException();
}
