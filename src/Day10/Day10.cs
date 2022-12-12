namespace AdventOfCode;

using AdventOfCode.CRT;

public class Day10Solution : Solution
{
    public Day10Solution() : base(2022, 10, "Cathode-Ray Tube") { }

    public override string SolvePartOne()
    {
        string[] instructions = input.Split(Environment.NewLine);
        Display display = new(6, 40);
        CPU cpu = new CPU(instructions, display);
        cpu.Run();
        display.Show();
        return cpu.SignalStrengths.Sum().ToString();
    }

    public override string SolvePartTwo()
    {
        return "See above.";
    }
}

