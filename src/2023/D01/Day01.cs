namespace AdventOfCode.Y2023.D01;

internal class Day01 : Problem
{
    public override async ValueTask SolveAsync()
    {
        var calibrationValues = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D01", "input.txt"));
        var calculations = new List<int>(capacity: calibrationValues.Length);

        foreach (var value in calibrationValues)
        {
            int firstDigit = 0;
            int lastDigit = 0;

            bool firstDigitFound = false;

            foreach (var character in value)
            {
                if (char.IsDigit(character))
                {
                    if (!firstDigitFound)
                    {
                        firstDigit = (int)char.GetNumericValue(character);
                        lastDigit = firstDigit;
                        firstDigitFound = true;
                    }

                    lastDigit = (int)char.GetNumericValue(character);
                }
            }

            var calculation = 10 * firstDigit + lastDigit;
            calculations.Add(calculation);
        }

        Console.WriteLine($"Result: {calculations.Sum()}");
    }
}
