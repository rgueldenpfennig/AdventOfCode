using System.Net.NetworkInformation;

namespace AdventOfCode.Y2023.D07;

internal class Day07 : Problem<int>
{
    internal readonly record struct Card(char Label)
    {
        public int Value { get; } = GetValueByLabel(Label);

        private static int GetValueByLabel(char label)
        {
            if (char.IsLetter(label))
            {
                return label switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 11,
                    'T' => 10,
                    _ => throw new InvalidOperationException(),
                };
            }

            return (int)char.GetNumericValue(label);
        }
    }

    internal struct Hand(Card[] cards, int bid)
    {
        public Card[] Cards { get; } = cards;

        public int Bid { get; } = bid;

        public HandType Type { get; } = GetHandType(cards);

        public int Rank { get; set; }

        private static HandType GetHandType(Card[] cards)
        {
            var groups = cards.GroupBy(c => c).ToArray();
            if (groups.Length == 1) return HandType.FiveOfAKind;
            if (groups.Length == 5) return HandType.HighCard;
            if (groups.Length == 4) return HandType.OnePair;

            if (groups.Length == 3)
            {
                if (groups.Any(g => g.Count() == 3))
                    return HandType.ThreeOfAKind;
                return HandType.TwoPair;
            }

            if (groups.Length == 2)
            {
                if (groups.Any(g => g.Count() == 4))
                    return HandType.FourOfAKind;
                if (groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2))
                    return HandType.FullHouse;
            }

            return HandType.HighCard;
        }

        public override readonly string ToString()
        {
            return $"{Type} - {Bid}";
        }
    }

    internal enum HandType
    {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1
    }

    public int SolveExample()
    {
        var hands = new List<Hand> {
            new Hand([
                new Card('3'),
                new Card('2'),
                new Card('T'),
                new Card('3'),
                new Card('K')],
            765),
            new Hand([
                new Card('T'),
                new Card('5'),
                new Card('5'),
                new Card('J'),
                new Card('5')],
            684),
            new Hand([
                new Card('K'),
                new Card('K'),
                new Card('6'),
                new Card('7'),
                new Card('7')],
            28),
            new Hand([
                new Card('K'),
                new Card('T'),
                new Card('J'),
                new Card('J'),
                new Card('T')],
            220),
            new Hand([
                new Card('Q'),
                new Card('Q'),
                new Card('Q'),
                new Card('J'),
                new Card('A')],
            483),
        };

        return CalculateTotalWinnings(hands);
    }

    public override async ValueTask<int> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        var hands = await ParseInputAsync();
        return CalculateTotalWinnings(hands);
    }

    public static async ValueTask<List<Hand>> ParseInputAsync()
    {
        var inputs = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D07", "input.txt"));
        var hands = new List<Hand>(capacity: inputs.Length);

        foreach (var input in inputs)
        {
            var values = input.Split(' ', StringSplitOptions.TrimEntries);
            var cards = new Card[5];

            int i = 0;
            foreach (var character in values[0])
            {
                cards[i] = new Card(character);
                i++;
            }

            hands.Add(new Hand(cards, Convert.ToInt32(values[1])));
        }

        return hands;
    }

    public static int CalculateTotalWinnings(List<Hand> hands)
    {
        hands.Sort(new HandComparer());

        var totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            var hand = hands[i];
            hand.Rank = i + 1;
            totalWinnings += hand.Rank * hand.Bid;
        }

        return totalWinnings;
    }

    internal class HandComparer : IComparer<Hand>
    {
        public int Compare(Hand x, Hand y)
        {
            //return x.Type - y.Type;
            if (x.Type < y.Type)
            {
                return -1;
            }

            if (x.Type > y.Type)
            {
                return 1;
            }

            for (int i = 0; i < x.Cards.Length; i++)
            {
                var xCard = x.Cards[i];
                var yCard = y.Cards[i];

                if (xCard.Value < yCard.Value)
                    return -1;

                if (xCard.Value > yCard.Value)
                    return 1;
            }

            throw new InvalidOperationException();
        }
    }
}
