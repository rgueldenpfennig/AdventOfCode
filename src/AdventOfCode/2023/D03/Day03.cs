using System.Diagnostics;

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
        return CalculateSum(lines);
    }

    public override async ValueTask<int> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        var inputLines = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D03", "input.txt"));
        return CalculateSum(inputLines);
    }

    public override async ValueTask<int> SolveSecondPartAsync(CancellationToken cancellationToken)
    {
        var inputLines = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D03", "input.txt"));
        return CalculateGearRatios(inputLines);
    }

    public static int CalculateSum(string[] lines)
    {
        var (numbers, symbols) = ParseInput(lines);

        for (int i = 0; i < numbers.Count; i++)
        {
            var number = numbers[i];
            var adjacentSymbols = symbols.Where(s =>
                (s.Line == number.Line || s.Line == number.Line - 1 || s.Line == number.Line + 1)
                && s.Position >= number.Start - 1 && s.Position <= number.End + 1);

            var count = adjacentSymbols.Count();
            if (count > 0)
            {
                number.HasAdjacentSymbol = true;
            }
        }

        return numbers.Where(n => n.HasAdjacentSymbol).Select(n => n.Value).Sum();
    }

    public static int CalculateGearRatios(string[] lines)
    {
        var (numbers, symbols) = ParseInput(lines);

        int result = 0;
        foreach (var gear in symbols.Where(s => s.Value == '*'))
        {
            var adjacentNumbers = numbers.Where(n =>
                (n.Line == gear.Line || n.Line == gear.Line - 1 || n.Line == gear.Line + 1)
                && gear.Position >= n.Start - 1 && gear.Position <= n.End + 1);

            if (adjacentNumbers.Count() == 2)
            {
                result += adjacentNumbers.Select(n => n.Value).Aggregate(1, (x, y) => x * y);
            }
        }

        return result;
    }

    private static (List<Number>, List<Symbol>) ParseInput(string[] lines)
    {
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();

        var lineNumber = 0;
        foreach (var line in lines)
        {
            lineNumber++;
            var numberRaw = new List<char>();
            for (int i = 0; i <= line.Length; i++)
            {
                char character;
                if (i == line.Length)
                {
                    character = '.';
                }
                else
                {
                    character = line[i];
                }

                if ((character == '.' || !char.IsNumber(character)) && numberRaw.Count > 0)
                {
                    numbers.Add(new Number(
                        Convert.ToInt32(new string(numberRaw.ToArray())),
                        lineNumber,
                        i - numberRaw.Count,
                        i - 1));
                    numberRaw.Clear();
                }

                if (char.IsNumber(character))
                {
                    numberRaw.Add(character);
                }
                else if (character != '.')
                {
                    symbols.Add(new(character, lineNumber, i));
                }
            }
        }

        return (numbers, symbols);
    }
}
