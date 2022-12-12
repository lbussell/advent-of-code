namespace AdventOfCode;

using AdventOfCode.Day09Util;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();

        Solution[] Days = {
            // new Day01Solution(),
            // new Day02Solution(),
            // new Day02Take2Solution(),
            // new Day04Solution(),
            // new Day05Solution(),
            // new Day06Solution(),
            // new Day07Solution(),
            // new Day08Solution(),
            // new Day09Solution(),
            new Day10Solution(),
        };

        foreach (Solution day in Days)
        {
            day.SolveAndPrintAll();
        }
    }
}
