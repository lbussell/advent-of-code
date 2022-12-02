namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();

        Solution[] Days = {
            new Day01Solution(),
            new Day02Solution(),
            new Day02Take2Solution(),
        };

        foreach (Solution day in Days)
        {
            day.SolveAndPrintAll();
        }
    }
}
