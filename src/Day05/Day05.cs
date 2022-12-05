namespace AdventOfCode;

public class Day05Solution : Solution
{

    public Day05Solution() : base(2022, 5, "Supply Stacks") { }

    public override string SolvePartOne() 
    {
        return Solve(false);
    }
    
    public override string SolvePartTwo()
    {
        return Solve(true);
    }

    private string Solve(bool retainOrder)
    {
        var inputs = input.Split(Environment.NewLine + Environment.NewLine);
        var crates = inputs[0].Split(Environment.NewLine).ToList();
        var instructions = inputs[1].Split(Environment.NewLine).Select(s => new Instruction(s));

        var numStacks = crates.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();
        var stackRows = crates.SkipLast(1);

        var crateStacks = new List<char>[numStacks];
        for (int s = 0; s < numStacks; s++)
            crateStacks[s] = new List<char>();

        foreach (string r in stackRows)
        {
            for (int s = 1; s <= numStacks; s++)
            {
                char c = r[charOffset(s)];
                if (c != ' ')
                {
                    crateStacks[s - 1] = crateStacks[s - 1].Prepend(c).ToList();
                }
            }
        }

        foreach (Instruction inst in instructions)
            Move(crateStacks, inst, retainOrder);

        return new String(crateStacks.Select(stack => stack.Last()).ToArray());
    }

    // src and dest are 1-indexed
    private static void Move(List<char>[] crateStacks, Instruction inst, bool retainOrder)
    {
        var _move = () =>
        {
            int toTake = retainOrder ? inst.N : 1;
            List<char> srcStack = crateStacks[inst.Src - 1];
            crateStacks[inst.Dest - 1].AddRange(srcStack.TakeLast(toTake));
            crateStacks[inst.Src - 1] = srcStack.SkipLast(toTake).ToList();
        };

        if (retainOrder)
            _move();
        else
            for (int i = 0; i < inst.N; i++) _move();
    }

    private static int charOffset(int stack) => (stack - 1) * 4 + 1;
}

public readonly struct Instruction
{
    public Instruction(string instructionString)
    {
        string[] data = instructionString.Split(' ');
        N = int.Parse(data[1]);
        Src = int.Parse(data[3]);
        Dest = int.Parse(data[5]);
    }

    public int N { get; }
    public int Src { get; }
    public int Dest { get; }
}