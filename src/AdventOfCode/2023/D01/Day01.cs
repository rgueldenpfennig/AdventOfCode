namespace AdventOfCode.Y2023.D01;

internal class Day01 : Problem<int>
{
    public override async ValueTask<int> SolveSecondPartAsync(CancellationToken cancellationToken)
    {
        var calibrationValues = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D01", "input.txt"));
        var calculations = new List<int>(capacity: calibrationValues.Length);

        foreach (var value in calibrationValues)
        {
            var calculation = CalculateResult(value);
            calculations.Add(calculation);
        }

        return calculations.Sum();
    }

    private readonly Dictionary<int, string> _letters = new()
    {
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" }
    };

    internal int CalculateResult(string input)
    {
        var span = input.AsSpan();
        int firstDigit = 0;
        int lastDigit = 0;

        bool firstDigitSet = false;

        for (int i = 0; i < input.Length; i++)
        {
            var character = input[i];
            if (char.IsDigit(character))
            {
                if (!firstDigitSet)
                {
                    firstDigit = (int)char.GetNumericValue(character);
                    firstDigitSet = true;
                }

                lastDigit = (int)char.GetNumericValue(character);
            }
            else
            {
                var range = span[i..];
                foreach (var letterPair in _letters)
                {
                    if (range.StartsWith(letterPair.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!firstDigitSet)
                        {
                            firstDigit = letterPair.Key;
                            firstDigitSet = true;
                        }

                        lastDigit = letterPair.Key;
                        break;
                    }
                }
            }
        }

        return 10 * firstDigit + lastDigit;
    }
}
