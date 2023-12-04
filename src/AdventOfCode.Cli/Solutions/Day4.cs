namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day4(IConfig config) : SolutionWithTextInput(config)
{
    public const StringSplitOptions SSOpts =
        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public override int Day => 4;

    public override string Name => "Scratchcards";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        return Input.Select(s => new ScratchOffCard(s)).Select(c => c.Score).Sum().ToString();
    }

    private string SolvePart2()
    {
        int totalCards = 0;

        ScratchOffCard[] cards = Input.Select(s => new ScratchOffCard(s)).ToArray();
        int[] matches = cards.Select(c => c.Matches).ToArray();

        Queue<int> cardQ = new();
        foreach (int i in Enumerable.Range(0, cards.Length))
        {
            cardQ.Enqueue(i);
        }

        while (cardQ.Count != 0)
        {
            int cardIndex = cardQ.Dequeue();
            for (int i = cardIndex + 1; i < cardIndex + 1 + matches[cardIndex]; i++)
            {
                cardQ.Enqueue(i);
            }

            totalCards += 1;
        }

        return totalCards.ToString();
    }
}

internal sealed class ScratchOffCard
{
    private readonly int[] _winningNumbers;

    private readonly int[] _scratchOffNumbers;

    public ScratchOffCard(string input)
    {
        // Example input:
        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 5
        int[][] inputs = input.Split(':')[1]
            .Split('|')
            .Select(s => s.Trim())
            .Select(s => s.Split(' ', Day4.SSOpts))
            .Select(s => s.Select(int.Parse).ToArray())
            .ToArray();

        _winningNumbers = inputs[0];
        _scratchOffNumbers = inputs[1];

        CardNumber = int.Parse(input.Split(':')[0]
            .Trim()
            .Split(' ', Day4.SSOpts)[1]);
    }

    // The first match is worth one point, then each match after that doubles the point value of the card.
    public int Score
    {
        get
        {
            int score = 0;
            foreach (int number in _scratchOffNumbers)
            {
                if (_winningNumbers.Contains(number))
                {
                    if (score == 0)
                    {
                        score = 1;
                        continue;
                    }

                    score *= 2;
                }
            }

            return score;
        }
    }

    public int Matches => _scratchOffNumbers.Count(n => _winningNumbers.Contains(n));

    public int CardNumber { get; init; }
}
