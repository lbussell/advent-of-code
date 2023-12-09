using System.Dynamic;
using System.Text;

namespace Bussell.AdventOfCode.Solutions;

internal sealed class Day7(IConfig config) : SolutionWithTextInput(config)
{
    public override int Day => 7;

    public override string Name => "Camel Cards";

    public override Func<string>[] Solutions => [
        SolvePart1,
        SolvePart2
    ];

    private string SolvePart1()
    {
        var hs = Input
            .Select(s => new Hand(s))
            .Order();

        foreach (var h in hs)
        {
            Console.WriteLine(h.Print());
        }

        return hs
            .Select((h, i) => h.Bet * (i + 1))
            .Sum()
            .ToString();
    }

    private string SolvePart2()
    {
        return string.Empty;
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

    private sealed class CardComparer : IComparer<char>
    {
        public static readonly char[] Order = "23456789TJQKA".ToCharArray();

        public int Compare(char x, char y)
        {
            return Array.IndexOf(Order, x) - Array.IndexOf(Order, y);
        }
    }

    private sealed class ReverseByteComparer : IComparer<byte>
    {
        public int Compare(byte x, byte y)
        {
            return y - x;
        }
    }

    private readonly record struct Hand : IComparable<Hand>
    {
        public readonly HandType Type { get; }

        public readonly int Bet { get; }

        public readonly int CardsRank { get; }

        public readonly char[] Cards { get; }

        public Hand(string input)
        {
            string[] splitInput = input.Split(' ');
            Bet = int.Parse(splitInput[1]);
            Cards = splitInput[0].ToCharArray();
            Type = CalculateHandType(Cards);
            CardsRank = CardsToBits(Cards);
            // Console.WriteLine(TieBreaker(_cards));
            // Console.WriteLine($"{string.Join(',', _cards)}: {Type}");
        }

        private static int CardsToBits(char[] cards)
        {
            int b = 0;
            foreach (char c in cards)
            {
                b = (b << 4) + Array.IndexOf(CardComparer.Order, c);
            }

            return b;
        }

        private static HandType CalculateHandType(char[] cards)
        {
            byte[] cardCounts = new byte[13];

            // Count up cards of each type
            foreach (char c in cards)
            {
                cardCounts[Array.IndexOf(CardComparer.Order, c)]++;
            }

            // Arrange highest card counts first
            Array.Sort(cardCounts, new ReverseByteComparer());

            HandType result = HandType.HighCard;
            for (int i = 0; i < cardCounts.Length; i += 1)
            {
                HandType current = TryGetFirstMatch(cardCounts[i..]);
                result = (int)result + current;
            }

            return result;
        }

        // Find the first match of the basic hand types in the byte array.
        private static HandType TryGetFirstMatch(IEnumerable<byte> cardCounts)
            => cardCounts.Where(b => b >= 2).FirstOrDefault() switch
            {
                2 => HandType.OnePair,
                3 => HandType.ThreeKind,
                4 => HandType.FourKind,
                5 => HandType.FiveKind,
                _ => HandType.HighCard,
            };

        public int CompareTo(Hand other)
        {
            // int result = other.Type - this.Type;
            // if (result == 0)
            // {
            //     result = other.CardsRank - this.CardsRank;
            // }

            // return result;
            int result = this.Type - other.Type;
            if (result == 0)
            {
                result = this.CardsRank - other.CardsRank;
            }

            return result;
        }

        public string Print()
        {
            return $"[{string.Join(',', Cards)}], {ToString()}";
        }
    }
}
