namespace AdventOfCode.Y2023.D03;

internal class Day03 : Problem<int>
{
    public record Number(int Value, int Line, int Start, int End)
    {
        public bool HasAdjacentSymbol { get; set; }
    }

    public readonly record struct Symbol(char Value, int Line, int Position);

    public static int SolveSample()
    {
        var input = """
            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
            """;

        var lines = input.Split('\n');
        var (numbers, symbols) = ParseInput(lines);

        for (int i = 0; i < numbers.Count; i++)
        {
            var number = numbers[i];
            var hasAdjacentSymbol = symbols.Exists(s =>
                (s.Line == number.Line || s.Line == number.Line - 1 || s.Line == number.Line + 1)
                && s.Position >= number.Start - 1 && s.Position <= number.End + 1);

            number.HasAdjacentSymbol = hasAdjacentSymbol;
        }

        return numbers.Where(n => n.HasAdjacentSymbol).Select(n => n.Value).Sum();
    }

    private static (List<Number>, List<Symbol>) ParseInput(string[] lines)
    {
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();

        var lineNumber = 1;
        foreach (var line in lines)
        {
            var numberRaw = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                var character = line[i];
                if (character == '\r') continue;
                if (character == '.')
                {
                    if (numberRaw.Count > 0)
                    {
                        numbers.Add(new Number(
                            Convert.ToInt32(new string(numberRaw.ToArray())),
                            lineNumber,
                            i - numberRaw.Count,
                            i - 1));
                        numberRaw.Clear();
                    }
                    continue;
                }

                if (!char.IsNumber(character))
                {
                    symbols.Add(new(character, lineNumber, i));
                }
                else
                {
                    numberRaw.Add(character);
                }
            }

            lineNumber++;
        }

        return (numbers, symbols);
    }

    public override ValueTask<int> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        return base.SolveFirstPartAsync(cancellationToken);
    }
}
