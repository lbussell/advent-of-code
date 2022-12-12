namespace AdventOfCode.CRT;
using System.Text;

public class CPU
{
    public IEnumerable<int> SignalStrengths { get; private set; }

    private int Ptr;
    private string[] Program;
    private int X;
    private int Clock;
    private int Wait;
    private Display Display;
    Action CurrentInstruction;
    private IEnumerable<(int c, int x)> DebugSignalStrengths;

    public CPU(string[] instructions, Display display) {
        Clock = 0;
        X = 1;
        Ptr = 0;
        Wait = 0;
        Program = instructions;
        CurrentInstruction = Noop;
        SignalStrengths = new List<int>();
        DebugSignalStrengths = new List<(int c, int x)>();
        Display = display;
    }

    public void Run()
    {
        while (Ptr < Program.Length || Wait > 0)
            Tick();
    }

    public void Tick()
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

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int pos = (Clock) % Display.W;
        if (X-1 <= pos && pos <= X+1)
        {
            Display.SetPixel(Clock);
        }
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

public class Display
{
    public int H { get; private set; }
    public int W { get; private set; }

    private bool[] _Display;

    public Display(int height, int width)
    {
        H = height;
        W = width;
        _Display = new bool[W*H];
    }

    public void SetPixel(int n)
    {
        if (n >= _Display.Length) return;
        _Display[n] = true;
    }

    public void Show()
    {
        for (int r = 0; r < H; r++)
        {
            StringBuilder sb = new();

            for (int c = 0; c < W; c++)
                sb.Append(_Display[r*W + c] ? '#' : '.');

            Console.WriteLine(sb.ToString());
        }
        Console.WriteLine();
    }
}