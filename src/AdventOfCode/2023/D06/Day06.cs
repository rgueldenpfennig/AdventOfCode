
namespace AdventOfCode.Y2023.D06;

internal class Day06 : Problem<long>
{
    internal record struct Race(long Time, long Distance)
    {
        public long GetDistanceByCharge(long chargeTime)
        {
            if (chargeTime == 0 || chargeTime >= Time) return 0;

            var diff = Time - chargeTime;
            return diff * chargeTime;
        }
    }

    public long SolveExample()
    {
        var races = new[] { new Race(7, 9), new Race(15, 40), new Race(30, 200) };
        return Solve(races);
    }

    public override ValueTask<long> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        var races = new[] { new Race(48, 296), new Race(93, 1928), new Race(85, 1236), new Race(95, 1391) };
        return ValueTask.FromResult(Solve(races));
    }

    public override ValueTask<long> SolveSecondPartAsync(CancellationToken cancellationToken)
    {
        var races = new[] { new Race(48938595, 296192812361391) };
        return ValueTask.FromResult(Solve(races));
    }

    public static long Solve(Race[] races)
    {
        var possibilities = new List<long>(capacity: races.Length);

        foreach (var race in races)
        {
            long minimumChargeTime;
            long maximumChargeTime;
            for (long i = 1; ; i++)
            {
                var distance = race.GetDistanceByCharge(i);
                if (distance > race.Distance)
                {
                    minimumChargeTime = i;
                    break;
                }
            }

            for (long i = race.Time - 1; ; i--)
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

        return possibilities.Aggregate(1L, (x, y) => x * y);
    }
}
