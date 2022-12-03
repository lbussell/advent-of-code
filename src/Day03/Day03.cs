namespace AdventOfCode;

public class Day03Solution : Solution
{

    public Day03Solution() : base(2022, 3, "Rucksack Reorganization") { }

    public override string SolvePartOne()
    {
        return input.Split(Environment.NewLine)
            .Select(r => (r.Substring(0, r.Length / 2), r.Substring(r.Length / 2, r.Length / 2)))
            .Select(r => r.Item1.Where(c => r.Item2.Contains(c)).FirstOrDefault())
            .Select(c => c < 'a' ? c - 'A' + 27 : c - 'a' + 1)
            .Sum().ToString();
    }

    public override string SolvePartTwo()
    {
        return Batch(input.Split(Environment.NewLine), 3)
            .Select(batch => batch[0].Where(c => batch[1].Contains(c) && batch[2].Contains(c)).FirstOrDefault())
            .Select(c => c < 'a' ? c - 'A' + 27 : c - 'a' + 1)
            .Sum().ToString();
    }

    private static IEnumerable<string[]> Batch(IEnumerable<string> lines, int batchSize)
    {
        int numLines = lines.Count();
        for (int i = 0; i < numLines / batchSize; i++)
        {
            yield return lines.Skip(i * batchSize).Take(batchSize).ToArray();
        }
    }
}
