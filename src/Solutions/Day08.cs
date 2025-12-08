// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.Numerics;

namespace AdventOfCode.Solutions.Day08;

internal readonly record struct Edge
{
    public Vector3 A { get; }
    public Vector3 B { get; }
    public float Distance { get; }

    public Edge(Vector3 a, Vector3 b)
    {
        // Ensure A is always the vector with the larger magnitude for equality
        if (Vector3.CompareMagnitude(a, b) >= 0)
        {
            A = a;
            B = b;
        }
        else
        {
            A = b;
            B = a;
        }

        Distance = Vector3.Distance(A, B);
    }
}

internal sealed class Day08Part1() : Solution(2025, 8, 1)
{
    public override string Solve(string input)
    {
        var boxes = input.ToLines().Select(Vector3.Parse).ToArray();

        // Build up graph of edges
        HashSet<Edge> edges = [];
        foreach (var boxA in boxes)
        {
            foreach (var boxB in boxes)
            {
                if (boxA == boxB) continue;
                var edge = new Edge(boxA, boxB);
                edges.Add(edge);
            }
        }

        // Sort edges by distance to get the shortest connections
        var edgeList = edges.ToList();
        edgeList.Sort((e1, e2) => e1.Distance.CompareTo(e2.Distance));

        // Put each box into its own circuit initially
        List<HashSet<Vector3>> circuits = boxes.Select(box => new HashSet<Vector3> { box }).ToList();
        HashSet<Vector3> GetCircuitFor(Vector3 box) => circuits.First(c => c.Contains(box));

        var iterations = 1000; // For the example input, this should be 10
        foreach (var edge in edgeList.Take(iterations))
        {
            var circuitA = GetCircuitFor(edge.A);
            var circuitB = GetCircuitFor(edge.B);

            // If they aren't in the same circuit, then merge them together
            if (circuitA != circuitB)
            {
                circuitA.UnionWith(circuitB);
                circuits.Remove(circuitB);
            }
        }

        // Multiply the sizes of the three largest circuits
        return circuits.OrderByDescending(c => c.Count)
                       .Take(3)
                       .Select(c => c.Count)
                       .Product()
                       .ToString();
    }

    public override IEnumerable<Example> Examples { get; } = [Example];

    internal static Example Example => new(
        """
        162,817,812
        57,618,57
        906,360,560
        592,479,940
        352,342,300
        466,668,158
        542,29,236
        431,825,988
        739,650,466
        52,470,668
        216,146,977
        819,987,18
        117,168,530
        805,96,715
        346,949,466
        970,615,88
        941,993,340
        862,61,35
        984,92,344
        425,690,689
        """,
        "40");
}

internal sealed class Day08Part2() : Solution(2025, 8, 2)
{
    public override string Solve(string input)
    {
        var boxes = input.ToLines().Select(Vector3.Parse).ToArray();

        // Build up graph of edges
        HashSet<Edge> edges = [];
        foreach (var boxA in boxes)
        {
            foreach (var boxB in boxes)
            {
                if (boxA == boxB) continue;
                var edge = new Edge(boxA, boxB);
                edges.Add(edge);
            }
        }

        // Sort edges by distance to get the shortest connections
        var edgeList = edges.ToList();
        edgeList.Sort((e1, e2) => e1.Distance.CompareTo(e2.Distance));

        // Put each box into its own circuit initially
        List<HashSet<Vector3>> circuits = boxes.Select(box => new HashSet<Vector3> { box }).ToList();
        HashSet<Vector3> GetCircuitFor(Vector3 box) => circuits.First(c => c.Contains(box));

        var result = "";
        foreach (var edge in edgeList)
        {
            var circuitA = GetCircuitFor(edge.A);
            var circuitB = GetCircuitFor(edge.B);

            // If they aren't in the same circuit, then merge them together
            if (circuitA != circuitB)
            {
                circuitA.UnionWith(circuitB);
                circuits.Remove(circuitB);
            }

            if (circuits.Count == 1)
            {
                result = "Last edge connecting circuits: " + edge;
                break;
            }
        }

        // Multiply the sizes of the three largest circuits
        return result;
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day08Part1.Example with { ExpectedOutput = "" }];
}

internal static class Day08Extensions
{
    extension(Vector3 v)
    {
        public static Vector3 Parse(string input)
        {
            var parts = input.Split(',');
            return new Vector3(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2]));
        }

        public static float CompareMagnitude(Vector3 a, Vector3 b)
        {
            var magA = a.Length();
            var magB = b.Length();
            return magA.CompareTo(magB);
        }
    }

    extension(IEnumerable<int> source)
    {
        public int Product()
        {
            var product = 1;
            foreach (var num in source)
            {
                product *= num;
            }
            return product;
        }
    }
}

internal static class InputExtensions
{
    private static readonly StringSplitOptions _sso = StringSplitOptions.RemoveEmptyEntries
                                                    | StringSplitOptions.TrimEntries;

    extension(string input)
    {
        public string[] ToLines() => input.Split(Environment.NewLine, _sso);
    }
}
