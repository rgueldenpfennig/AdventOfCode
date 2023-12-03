namespace AdventOfCode.Y2023.D01;

internal class Day01 : Problem
{
    public override async ValueTask SolveAsync()
    {
        var calibrationValues = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D01", "input.txt"));
        var calculations = new List<int>(capacity: calibrationValues.Length);

        foreach (var value in calibrationValues)
        {
            char firstDigit = '0';
            char lastDigit = '0';

            bool firstDigitFound = false;

            foreach (var character in value)
            {
                if (char.IsDigit(character))
                {
                    if (!firstDigitFound)
                    {
                        firstDigit = character;
                        lastDigit = character;
                        firstDigitFound = true;
                    }

                    lastDigit = character;
                }
            }

            var calculation = (int)(10 * char.GetNumericValue(firstDigit) + char.GetNumericValue(lastDigit));
            calculations.Add(calculation);
        }

        Console.WriteLine($"Result: {calculations.Sum()}");
    }
}
