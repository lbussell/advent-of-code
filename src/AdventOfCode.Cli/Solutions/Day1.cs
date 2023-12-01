namespace Bussell.AdventOfCode.Solutions;

using Result = (int Index, int Value);

internal class Day1 : SolutionWithTextInput
{
    private static readonly string[] Words = [ "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" ];

    public override int Day => 1;

    public override string Name => "Trebuchet?!";

    public override string SolvePart1()
    {
        int sum = 0;

        foreach (ReadOnlySpan<char> line in Input)
        {
            int firstIndex = line.IndexOfAnyInRange('0', '9');
            int lastIndex = line.LastIndexOfAnyInRange('0', '9');
            Span<char> calibrationValue = [line[firstIndex], line[lastIndex]];
            sum += int.Parse(calibrationValue);
        }

        return sum.ToString();
    }

    public override string SolvePart2()
    {
        int sum = 0;
        int lineNumber = 1;

        foreach (ReadOnlySpan<char> line in Input)
        {
            char firstNumber = (char)(GetFirstNumber(line) + '0');
            char lastNumber = (char)(GetLastNumber(line) + '0');
            ReadOnlySpan<char> calibrationValue = [firstNumber, lastNumber];
            int i = int.Parse(calibrationValue);
            Console.WriteLine($"{lineNumber++} {line}: {i}");
            sum += i;
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
