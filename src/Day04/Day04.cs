namespace AdventOfCode;

public class Day04Solution : Solution
{

    public Day04Solution() : base(2022, 4, "Camp Cleanup") { }

    public override string SolvePartOne() =>
        input.Split(Environment.NewLine)
            .Select(ParsePairString)
            .Where(PairsContainEachother)
            .Count()
            .ToString();

    public override string SolvePartTwo() =>
        input.Split(Environment.NewLine)
            .Select(ParsePairString)
            .Where(PairsOverlap)
            .Count()
            .ToString();

    private static int[][] ParsePairString(string p) =>
        p.Split(',').Select(q => q.Split('-').Select(int.Parse).ToArray()).ToArray();

    private static bool PairsContainEachother(int[][] p) =>
        ((p[1][0] <= p[0][0]) && (p[0][1] <= p[1][1]))
            || ((p[0][0] <= p[1][0]) && (p[1][1] <= p[0][1]));

    private static bool PairsOverlap(int[][] p) =>
        !(((p[0][0] < p[1][0]) && (p[0][1] < p[1][0]))
            || ((p[1][0] < p[0][0]) && (p[1][1] < p[0][0])));
}
