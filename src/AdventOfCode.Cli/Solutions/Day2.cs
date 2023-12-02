namespace Bussell.AdventOfCode.Solutions;

using Hand = (int Red, int Green, int Blue);

internal sealed class Day2(IConfig config) : SolutionWithTextInput(config)
{

    public override int Day => 2;

    public override string Name => "Cube Conundrum";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        IEnumerable<Game> games = GetGames(Input);
        return games
            .Select((g, i) => g.IsPossible() ? i + 1 : 0)
            .Sum()
            .ToString();
    }

    private string SolvePart2()
    {
        IEnumerable<Game> games = GetGames(Input);
        return games
            .Select(g => g.GetMinimumCubesPower())
            .Sum()
            .ToString();
    }

    private static IEnumerable<Game> GetGames(IEnumerable<string> input)
        => input.Select(s => new Game(s.Split(':')[1].Trim()));
}

internal sealed class Game(string input)
{
    private static readonly int[] Limits = [12, 13, 14];

    public IEnumerable<Hand> Hands { get; } = input.Split(';')
            .Select(s => s.Trim())
            .Select(ParseHand);

    public bool IsPossible()
    {
        foreach (Hand hand in Hands)
        {
            if (hand.Red > Limits[0] || hand.Green > Limits[1] || hand.Blue > Limits[2])
            {
                return false;
            }
        }

        return true;
    }

    public int GetMinimumCubesPower()
    {
        int r = 0, g = 0, b = 0;
        foreach (Hand hand in Hands)
        {
            if (hand.Red > r)
            {
                r = hand.Red;
            }

            if (hand.Green > g)
            {
                g = hand.Green;
            }

            if (hand.Blue > b)
            {
                b = hand.Blue;
            }
        }

        return r * g * b;
    }

    private static Hand ParseHand(string input)
    {
        // Example: 3 blue, 4 red, 1 green
        var t = input.Split(',') // separate colors
            .Select(s => s.Trim().Split(' ')); // Separate color and number

        int r = 0, g = 0, b = 0;
        foreach (var pair in t)
        {
            int number = int.Parse(pair[0]);
            string color = pair[1];
            switch (color)
            {
                case "red":
                    r = number;
                    break;
                case "green":
                    g = number;
                    break;
                case "blue":
                    b = number;
                    break;
            }
        }

        return (r, g, b);
    }
}
