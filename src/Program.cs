namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();

        Solution[] Days = {
            new Day01(),
        };

        foreach (Solution day in Days)
        {
            day.SolveAndPrintAll();
        }
    }
}
