namespace AdventOfCode.CRT;

class CPU
{
    public IEnumerable<int> SignalStrengths { get; private set; }
    private IEnumerable<(int c, int x)> DebugSignalStrengths;

    private int Clock;
    private int X;
    private int Ptr;
    private int Wait;
    private string[] Program;
    Action CurrentInstruction;

    public CPU(string[] instructions) {
        Clock = 0;
        X = 1;
        Ptr = 0;
        Wait = 0;
        Program = instructions;
        CurrentInstruction = Noop;
        SignalStrengths = new List<int>();
        DebugSignalStrengths = new List<(int c, int x)>();
    }

    public void Run()
    {
        while (Ptr < Program.Length || Wait > 0)
            Tick();
        // if (Wait == 0)
        //     CurrentInstruction();
    }

    private void Tick()
    {
        Clock += 1;

        if ((Clock - 20) % 40 == 0)
        {
            DebugSignalStrengths = DebugSignalStrengths.Append((Clock, X));
            SignalStrengths = SignalStrengths.Append(Clock * X);
        }

        if (Wait <= 0)
        {
            CurrentInstruction = ReadInstruction();
        }
        
        Wait -= 1;

        if (Wait <= 0)
        {
            CurrentInstruction();
        }

        Console.WriteLine($"clock {Clock}, wait {Wait}, ptr {Ptr}, x {X}");
    }

    private Action ReadInstruction()
    {
        string[] instruction = Program[Ptr].Split(' ');
        Ptr += 1;
        switch (instruction)
        {
            case ["noop"]:
                Wait = 0;
                return Noop;
            case ["addx", _]:
                Wait = 2;
                return CurrentInstruction = AddX(int.Parse(instruction[1]));
            default:
                throw new ArgumentOutOfRangeException($"Instruction {Program[Ptr]} is not valid");
        }
    }

    private static void Noop() { return; }
    private Action AddX(int n) => () => X += n;
}