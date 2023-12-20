namespace AdventOfCode;

using AdventOfCode.Core;

internal sealed class Program
{
    // Input format: {input.txt path} {year} {day} {part}
    // Output format: answer1={part 1 answer}
    public static void Main(string[] args)
    {
        if (args.Length == 1)
        {
            RunAll(args[0]);
            return;
        }

        if (args.Length < 4)
        {
            throw new ArgumentException("Expected {input.txt path} {year} {day} {part}");
        }

        int year = int.Parse(args[1]);
        int day = int.Parse(args[2]);
        int part = int.Parse(args[3]);

        Run(args[0], year, day, part);
    }

    private static void RunAll(string inputFilePath)
    {
        SolutionProvider sp = new();
        IEnumerable<ISolution> solutions = sp.GetService<IEnumerable<ISolution>>();
        foreach (ISolution s in solutions)
        {
            for (int part = 0; part < 2; part += 1)
            {
                IEnumerable<string> input = File.ReadAllLines(Path.Combine(inputFilePath, $"{s.Day:D2}.txt"));
                string output = s.Solutions[part](input);
                Console.WriteLine($"Day {s.Day} part {part}: {output}");
            }
        }
    }

    private static void Run(string inputFile, int year, int day, int part)
    {
        SolutionProvider sp = new();
        ISolution solution = GetSolutions().First(s => s.Day == day);
        IEnumerable<string> input = File.ReadAllLines(inputFile);
        string output = solution.Solutions[part - 1](input);
        Console.WriteLine($"answer{part}={output}");
    }

    private static IEnumerable<ISolution> GetSolutions()
    {
        SolutionProvider sp = new();
        return sp.GetService<IEnumerable<ISolution>>();
    }
}
