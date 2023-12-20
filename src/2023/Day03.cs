namespace AdventOfCode.Solutions2023;

using Size = (int Height, int Width);

public sealed class Day03 : ISolution
{
    public int Year => 2023;

    public int Day => 3;

    public string Name => "Gear Ratios";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1(IEnumerable<string> input)
    {
        char[][] engine = input.Select(s => s.ToCharArray()).ToArray();
        Size engineSize = (engine.Length, engine[0].Length);

        int sumOfPartNumbers = 0;

        for (int r = 0; r < engineSize.Height; r++)
        {
            for (int c = 0; c < engineSize.Width; c++)
            {
                if (char.IsDigit(engine[r][c]))
                {
                    int numberLength = 0;
                    while (char.IsDigit(engine[r][c + numberLength]))
                    {
                        numberLength += 1;
                        if (c + numberLength >= engineSize.Width)
                        {
                            break;
                        }
                    }

                    // Check for symbols around the number
                    bool shouldCountPart = false;
                    for (int r2 = r - 1; r2 <= r + 1; r2++)
                    {
                        for (int c2 = c - 1; c2 < c + numberLength + 1; c2++)
                        {
                            if (r2 == r && c2 == c)
                            {
                                continue;
                            }

                            if (r2 < 0 || r2 >= engineSize.Height || c2 < 0 || c2 >= engineSize.Width)
                            {
                                continue;
                            }

                            if (r2 == r && c2 >= c && c2 < c + numberLength)
                            {
                                continue;
                            }

                            if (engine[r2][c2] != '.')
                            {
                                shouldCountPart = true;
                            }
                        }
                    }

                    int partNumber = int.Parse(engine[r].AsSpan()[c..(c + numberLength)]);

                    if (shouldCountPart)
                    {
                        sumOfPartNumbers += partNumber;
                    }

                    c += numberLength;
                }
            }
        }

        return sumOfPartNumbers.ToString();
    }

    private string SolvePart2(IEnumerable<string> input)
    {
        char[][] engine = input.Select(s => s.ToCharArray()).ToArray();
        Size engineSize = (engine.Length, engine[0].Length);

        int sum = 0;
        for (int r = 0; r < engineSize.Height; r++)
        {
            for (int c = 0; c < engineSize.Width; c++)
            {
                if (engine[r][c] == '*')
                {
                    int gr = GetGearRatio(engine, engineSize, r, c);
                    if (gr != -1)
                    {
                        sum += gr;
                    }
                }
            }
        }

        return sum.ToString();
    }

    private static int GetGearRatio(char[][] engine, Size engineSize, int rStart, int cStart)
    {
        int gear1 = -1;
        int gear2 = -1;

        for (int r = rStart - 1; r < rStart + 2; r++)
        {
            for (int c = cStart - 1; c < cStart + 2; c++)
            {
                if (!CheckBounds(engineSize, r, c))
                {
                    continue;
                }

                if (char.IsDigit(engine[r][c]))
                {
                    int n = ExpandNumber(engine, engineSize, r, ref c);
                    if (gear1 == -1)
                    {
                        gear1 = n;
                    }
                    else if (gear2 == -1)
                    {
                        gear2 = n;
                        return gear1 * gear2;
                    }
                }
            }
        }

        return -1;
    }

    private static int ExpandNumber(char[][] engine, Size engineSize, int r, ref int c)
    {
        int left = c, right = c;

        while (left >= 0 && char.IsDigit(engine[r][left]))
        {
            left -= 1;
        }

        while (c < engineSize.Width && char.IsDigit(engine[r][c]))
        {
            c += 1;
        }

        int number = int.Parse(engine[r].AsSpan()[(left + 1)..c]);
        return number;
    }

    private static bool CheckBounds(Size engineSize, int r, int c)
        => r >= 0 && r < engineSize.Height && c >= 0 && c < engineSize.Width;
}
