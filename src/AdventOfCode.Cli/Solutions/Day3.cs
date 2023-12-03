namespace Bussell.AdventOfCode.Solutions;

using Size = (int Height, int Width);

internal sealed class Day3 : SolutionWithTextInput
{
    private readonly char[][] _engine;

    private readonly Size _engineSize;

    public Day3(IConfig config)
        : base(config)
    {
        _engine = Input.Select(s => s.ToCharArray()).ToArray();
        _engineSize = (_engine.Length, _engine[0].Length);
    }

    public override int Day => 3;

    public override string Name => "Gear Ratios";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        int sumOfPartNumbers = 0;

        for (int r = 0; r < _engineSize.Height; r++)
        {
            for (int c = 0; c < _engineSize.Width; c++)
            {
                if (char.IsDigit(_engine[r][c]))
                {
                    int numberLength = 0;
                    while (char.IsDigit(_engine[r][c + numberLength]))
                    {
                        numberLength += 1;
                        if (c + numberLength >= _engineSize.Width)
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

                            if (r2 < 0 || r2 >= _engineSize.Height || c2 < 0 || c2 >= _engineSize.Width)
                            {
                                continue;
                            }

                            if (r2 == r && c2 >= c && c2 < c + numberLength)
                            {
                                continue;
                            }

                            if (_engine[r2][c2] != '.')
                            {
                                shouldCountPart = true;
                            }
                        }
                    }

                    int partNumber = int.Parse(_engine[r].AsSpan()[c..(c + numberLength)]);

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

    private string SolvePart2()
    {
        int sum = 0;
        for (int r = 0; r < _engineSize.Height; r++)
        {
            for (int c = 0; c < _engineSize.Width; c++)
            {
                if (_engine[r][c] == '*')
                {
                    int gr = GetGearRatio(r, c);
                    if (gr != -1)
                    {
                        sum += gr;
                    }
                }
            }
        }

        return sum.ToString();
    }

    private int GetGearRatio(int rStart, int cStart)
    {
        int gear1 = -1;
        int gear2 = -1;

        for (int r = rStart - 1; r < rStart + 2; r++)
        {
            for (int c = cStart - 1; c < cStart + 2; c++)
            {
                if (!CheckBounds(r, c))
                {
                    continue;
                }

                if (char.IsDigit(_engine[r][c]))
                {
                    int n = ExpandNumber(r, ref c);
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

    private int ExpandNumber(int r, ref int c)
    {
        int left = c, right = c;

        while (left >= 0 && char.IsDigit(_engine[r][left]))
        {
            left -= 1;
        }

        while (c < _engineSize.Width && char.IsDigit(_engine[r][c]))
        {
            c += 1;
        }

        int number = int.Parse(_engine[r].AsSpan()[(left + 1)..c]);
        return number;
    }

    private bool CheckBounds(int r, int c)
        => r >= 0 && r < _engineSize.Height && c >= 0 && c < _engineSize.Width;
}
