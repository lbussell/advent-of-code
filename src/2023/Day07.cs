namespace AdventOfCode.Solutions2023;

using AdventOfCode.Core;

public sealed class Day07 : ISolution
{
    public int Year => 2023;

    public int Day => 7;

    public string Name => "Camel Cards";

    public Func<IEnumerable<string>, string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private static string SolvePart1(IEnumerable<string> input)
    {
        CardComparer cc = new();
        return input
            .Select(s => new Hand(s, cc, Part.One))
            .Order()
            .Select((h, i) => h.Bet * (i + 1))
            .Sum()
            .ToString();
    }

    private static string SolvePart2(IEnumerable<string> input)
    {
        CardComparer cc = new Part2CardComparer();
        return input
            .Select(s => new Hand(s, cc, Part.Two))
            .Order()
            .Select((h, i) => h.Bet * (i + 1))
            .Sum()
            .ToString();
    }

    [Flags]
    private enum HandType
    {
        HighCard  = 0b00000,
        OnePair   = 0b00001, // OnePair + OnePair = TwoPair
        TwoPair   = 0b00010,
        ThreeKind = 0b00100, // ThreeKind + OnePair = FullHouse
        FullHouse = 0b00101,
        FourKind  = 0b01000,
        FiveKind  = 0b10000,
    }

    private class CardComparer : IComparer<char>
    {
        public virtual char[] Order { get; } = "23456789TJQKA".ToCharArray();

        public int Compare(char x, char y)
        {
            return Array.IndexOf(Order, x) - Array.IndexOf(Order, y);
        }
    }

    private sealed class Part2CardComparer : CardComparer
    {
        public override char[] Order { get; } = "J23456789TQKA".ToCharArray();
    }

    private sealed class ReverseByteComparer : IComparer<byte>
    {
        public int Compare(byte x, byte y)
        {
            return y - x;
        }
    }

    private enum Part
    {
        One,
        Two,
    }

    private readonly record struct Hand : IComparable<Hand>
    {
        private readonly Part _part;
        private readonly CardComparer _comparer;

        public readonly HandType Type { get; }
        public readonly int Bet { get; }
        public readonly int CardsScore { get; }
        public readonly char[] Cards { get; }

        public Hand(string input, CardComparer comparer, Part part)
        {
            _part = part;
            _comparer = comparer;

            string[] splitInput = input.Split(' ');
            Bet = int.Parse(splitInput[1]);
            Cards = splitInput[0].ToCharArray();

            Type = GetHandType(Cards);
            CardsScore = GetScore(Cards);
        }

        private int GetScore(char[] cards)
        {
            int b = 0;
            foreach (char c in cards)
            {
                b = (b << 4) + Array.IndexOf(_comparer.Order, c);
            }

            return b;
        }

        private HandType GetHandType(char[] cards) => _part switch
            {
                Part.One => CalculatePart1HandType(cards),
                _ => GetPart2HandType(cards),
            };

        private HandType CalculatePart1HandType(char[] cards)
        {
            byte[] cardCounts = new byte[13];

            // Count up cards of each type
            foreach (char c in cards)
            {
                cardCounts[Array.IndexOf(_comparer.Order, c)]++;
            }

            // Arrange highest card counts first.
            // Using a custom comparer instead of LINQ's OrderByDescending here
            // halves the execution time.
            // It's also faster than Array.Sort() and then Reverse().
            Array.Sort(cardCounts, new ReverseByteComparer());
            return GetHandTypeBase(cardCounts);
        }

        private HandType GetPart2HandType(char[] cards)
        {
            byte[] cardCounts = new byte[13];
            int numJs = cards.Where(c => c == 'J').Count();
            cards = cards.Where(c => c != 'J').ToArray();

            foreach (char c in cards)
            {
                cardCounts[Array.IndexOf(_comparer.Order, c)] += 1;
            }

            Array.Sort(cardCounts, new ReverseByteComparer());

            // Add Js to the most frequent card
            cardCounts[0] = (byte)(cardCounts[0] + numJs);
            return GetHandTypeBase(cardCounts);
        }

        // Find the first match of the basic hand types in the byte array.
        private static HandType GetHandTypeBase(IEnumerable<byte> cardCounts)
        {
            HandType result = HandType.HighCard;
            foreach (byte count in cardCounts)
            {
                result = (int)result + count switch
                {
                    2 => HandType.OnePair,
                    3 => HandType.ThreeKind,
                    4 => HandType.FourKind,
                    5 => HandType.FiveKind,
                    _ => HandType.HighCard,
                };
            }

            return result;
        }

        public int CompareTo(Hand other)
        {
            int result = this.Type - other.Type;
            if (result == 0)
            {
                result = this.CardsScore - other.CardsScore;
            }

            return result;
        }
    }
}
