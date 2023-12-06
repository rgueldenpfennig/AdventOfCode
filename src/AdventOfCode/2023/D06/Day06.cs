namespace AdventOfCode.Y2023.D06;

internal class Day06 : Problem<uint>
{
    internal record struct Race(uint Time, uint Distance)
    {
        public uint GetDistanceByCharge(uint chargeTime)
        {
            if (chargeTime == 0 || chargeTime >= Time) return 0;

            var diff = Time - chargeTime;
            return diff * chargeTime;
        }
    }

    public uint SolveExample()
    {
        var races = new[] { new Race(7, 9), new Race(15, 40), new Race(30, 200) };
        var possibilities = new List<uint>(capacity: races.Length);

        foreach (var race in races)
        {
            uint minimumChargeTime;
            uint maximumChargeTime;
            for (uint i = 1; ; i++)
            {
                var distance = race.GetDistanceByCharge(i);
                if (distance > race.Distance)
                {
                    minimumChargeTime = i;
                    break;
                }
            }

            for (uint i = race.Time - 1; ; i--)
            {
                var distance = race.GetDistanceByCharge(i);
                if (distance > race.Distance)
                {
                    maximumChargeTime = i;
                    break;
                }
            }

            possibilities.Add(1 + maximumChargeTime - minimumChargeTime);
        }

        return possibilities.Aggregate(1U, (x, y) => x * y);
    }
}
