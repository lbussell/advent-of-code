namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day1(IConfig config) : SolutionWithTextInput(config)
{
    private static readonly string[] Words =
    [
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    ];

    public override int Day => 1;

    public override string Name => "Trebuchet?!";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2,
        SolvePart2Take2
    ];

    private string SolvePart1()
    {
        int sum = 0;

        foreach (ReadOnlySpan<char> s in Input)
        {
            sum += GetPart1CalibrationValue(s);
        }

        return sum.ToString();
    }

    private int GetPart1CalibrationValue(ReadOnlySpan<char> s)
    {
        int firstIndex = s.IndexOfAnyInRange('0', '9');
        int lastIndex = s.LastIndexOfAnyInRange('0', '9');
        Span<char> calibrationValue = [s[firstIndex], s[lastIndex]];
        return int.Parse(calibrationValue);
    }

    private string SolvePart2()
    {
        int sum = 0;

        foreach (ReadOnlySpan<char> line in Input)
        {
            char firstNumber = (char)(GetFirstNumber(line) + '0');
            char lastNumber = (char)(GetLastNumber(line) + '0');
            ReadOnlySpan<char> calibrationValue = [firstNumber, lastNumber];
            sum += int.Parse(calibrationValue);
        }

        return sum.ToString();
    }

    private string SolvePart2Take2()
    {
        int sum = 0;
        foreach (string line in Input)
        {
            string s = line.Replace("zero", "0o");
            s = s.Replace("one", "o1e");
            s = s.Replace("two", "t2o");
            s = s.Replace("three", "t3e");
            s = s.Replace("four", "4");
            s = s.Replace("five", "5e");
            s = s.Replace("six", "6");
            s = s.Replace("seven", "7n");
            s = s.Replace("eight", "e8t");
            s = s.Replace("nine", "n9e");
            sum += GetPart1CalibrationValue(s);
        }

        return sum.ToString();
    }

    private static int GetFirstNumber(ReadOnlySpan<char> s)
    {
        int firstIndex = s.IndexOfAnyInRange('0', '9');
        int number = firstIndex >= 0 ? int.Parse([s[firstIndex]]) : -1;
        int lowestIndex = firstIndex >= 0 ? firstIndex : int.MaxValue;

        for (int i = 0; i <= 9; i++)
        {
            int found = s.IndexOf(Words[i]);
            if (found >= 0 && found < lowestIndex)
            {
                lowestIndex = found;
                number = i;
            }
        }

        return number;
    }

    private static int GetLastNumber(ReadOnlySpan<char> s)
    {
        int lastIndex = s.LastIndexOfAnyInRange('0', '9');
        int number = lastIndex >= 0 ? int.Parse([s[lastIndex]]) : -1;
        int highestIndex = lastIndex >= 0 ? lastIndex : -1;

        for (int i = 0; i <= 9; i++)
        {
            int currentIndex = s.LastIndexOf(Words[i]);
            if (currentIndex > highestIndex)
            {
                highestIndex = currentIndex;
                number = i;
            }
        }

        return number;
    }
}
