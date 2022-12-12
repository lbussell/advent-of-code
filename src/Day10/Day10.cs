namespace AdventOfCode;

using AdventOfCode.CRT;

public class Day10Solution : Solution
{
    public Day10Solution() : base(2022, 10, "Cathode-Ray Tube") { }

    public override string SolvePartOne()
    {
        string[] instructions = input.Split(Environment.NewLine);
        CPU cpu = new CPU(instructions);
        cpu.Run();
        return cpu.SignalStrengths.Sum().ToString();
    }

    public override string SolvePartTwo()
    {
        return "unsolved";
    }
}

