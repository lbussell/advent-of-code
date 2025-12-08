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
    private const bool DebugOutput = false;

    public override string Solve(string input)
    {
        var edgeA = new Edge(new Vector3(1, 2, 3), new Vector3(4, 5, 6));
        var edgeB = new Edge(new Vector3(4, 5, 6), new Vector3(1, 2, 3));
        Console.WriteLine($"Edge A: {edgeA}");
        Console.WriteLine($"Edge B: {edgeB}");
        Console.WriteLine($"Edges equal: {edgeA == edgeB}");

        var boxes = input.ToLines()
                         .Select(Vector3.Parse)
                         .ToArray();

        HashSet<Edge> edges = [];

        // Build up graph of edges
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
        Console.WriteLine(string.Join(Environment.NewLine, edgeList.Take(10)));

        List<HashSet<Vector3>> clusters = [];
        HashSet<Vector3>? GetFromClusters(Vector3 box) => clusters.FirstOrDefault(c => c.Contains(box));

        foreach (var edge in edgeList.Take(1000))
        {
            var clusterA = GetFromClusters(edge.A);
            var aAlreadyInCluster = clusterA is not null;
            var clusterB = GetFromClusters(edge.B);
            var bAlreadyInCluster = clusterB is not null;

            switch (aAlreadyInCluster, bAlreadyInCluster)
            {
                // Neither point is in a cluster yet.
                // Create a new cluster.
                case (false, false):
                    if (DebugOutput) Console.WriteLine($"Creating new cluster with edge: {edge.A}-{edge.B}");
                    var newCluster = new HashSet<Vector3> { edge.A, edge.B };
                    clusters.Add(newCluster);
                    break;

                // One point is already in a cluster.
                // Add the other point to the existing cluster.
                case (true, false):
                    if (DebugOutput) Console.WriteLine($"Adding {edge.B} to existing cluster with {edge.A}");
                    clusterA!.Add(edge.B);
                    break;
                case (false, true):
                    if (DebugOutput) Console.WriteLine($"Adding {edge.A} to existing cluster with {edge.B}");
                    clusterB!.Add(edge.A);
                    break;

                // Both points are already in clusters.
                // If they are not the same cluster, merge them together.
                case (true, true) when clusterA != clusterB:
                    if (DebugOutput) Console.WriteLine($"Merging clusters containing {edge.A} and {edge.B}");
                    clusterA!.UnionWith(clusterB!);
                    clusters.Remove(clusterB!);
                    break;

                default:
                    // Both points are already in the same cluster, do nothing.
                    if (DebugOutput) Console.WriteLine($"Both points {edge.A} and {edge.B} are already in the same cluster, skipping.");
                    break;
            }
        }

        // Get the sizes of the three largest clusters
        var largestClusters = clusters.OrderByDescending(c => c.Count)
                                      .Take(3)
                                      .ToArray();

        Console.WriteLine($"Number of clusters: {clusters.Count}");
        Console.WriteLine($"Sizes of three largest clusters: {string.Join(", ", largestClusters.Select(c => c.Count))}");

        return largestClusters.Select(c => c.Count).Product().ToString();
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
        return "";
    }

    public override IEnumerable<Example> Examples { get; } =
        [Day08Part1.Example with { ExpectedOutput = "TODO" }];
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
