namespace AdventOfCode.Y2023.D04;

internal class Day04 : Problem<int>
{
    internal record Card(int Id, int[] WinningNumbers, int[] OwnedNumbers);

    public override async ValueTask<int> SolveFirstPartAsync()
    {
        var cards = await GetCardsFromInputAsync();
        var points = 0;

        foreach (var card in cards)
        {
            var intersect = card.WinningNumbers.Intersect(card.OwnedNumbers).ToArray();
            if (intersect.Length == 1)
            {
                points++;
            }
            else if (intersect.Length > 0)
            {
                points += Convert.ToInt32(Math.Pow(2, intersect.Length - 1));
            }
        }

        return points;
    }

    internal async ValueTask<List<Card>> GetCardsFromInputAsync()
    {
        var cardInputs = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D04", "input.txt"));
        var cards = new List<Card>(capacity: cardInputs.Length);

        foreach (var cardInput in cardInputs)
        {
            cards.Add(ParseCard(cardInput));
        }

        return cards;
    }

    private static Card ParseCard(string input)
    {
        // Card   1: 99 65 21  4 72 20 77 98 27 70 | 34 84 74 18 41 45 72  2  1 75 52 47 50 93 25 10 79 87 42 69  8 12 54 96 92
        var split = input.Split(':', StringSplitOptions.TrimEntries);
        var cardSplit = split[0];
        var numbersSplit = split[1].Split('|');

        var cardId = Convert.ToInt32(cardSplit.AsSpan()[5..].Trim().ToString());

        var winningNumbersRaw = numbersSplit[0].Split(' ', StringSplitOptions.TrimEntries ^ StringSplitOptions.RemoveEmptyEntries);
        var ownedNumbersRaw = numbersSplit[1].Split(' ', StringSplitOptions.TrimEntries ^ StringSplitOptions.RemoveEmptyEntries);

        var winningNumbers = winningNumbersRaw.Select(s => Convert.ToInt32(s)).ToArray();
        var ownedNumbers = ownedNumbersRaw.Select(s => Convert.ToInt32(s)).ToArray();

        return new Card(cardId, winningNumbers, ownedNumbers);
    }
}
