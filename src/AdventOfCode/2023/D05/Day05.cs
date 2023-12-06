
using System.Diagnostics;

namespace AdventOfCode.Y2023.D05;

internal class Day05 : Problem<ulong>
{
    internal record struct Seed(int Id, ulong Start, ulong Length);

    internal record struct Range(ulong DestinationRangeStart, ulong SourceRangeStart, ulong RangeLength)
    {
        public static readonly Range Default = new ();

        public ulong MaxSourceRange { get; } = SourceRangeStart + (RangeLength - 1);

        public ulong MaxDestinationRange { get; } = DestinationRangeStart + (RangeLength - 1);

        public bool IsMatch(ulong source)
        {
            return source >= SourceRangeStart && source <= MaxSourceRange;
        }
    }

    internal record Map(string Name, IList<Range> Ranges)
    {
        public ulong MapToDestination(ulong source)
        {
            var range = GetMatchingRange(source);
            if (range == Range.Default)
                return source;

            if (range.SourceRangeStart == source)
                return range.DestinationRangeStart;

            if (source > range.MaxSourceRange)
                return source;

            var sourceDiff = source - range.SourceRangeStart;
            return range.DestinationRangeStart + sourceDiff;
        }

        private Range GetMatchingRange(ulong source)
        {
            return Ranges.FirstOrDefault(r => r.IsMatch(source));
        }
    }

    public ulong SolveExample()
    {
        var seedToSoil = new Map("seed-to-soil", new[] { new Range(50, 98, 2), new Range(52, 50, 48) });
        var soilToFertilizer = new Map("soil-to-fertilizer", new[] { new Range(0, 15, 37), new Range(37, 52, 2), new Range(39, 0, 15) });
        var fertilizerToWater = new Map("fertilizer-to-water", new[] { new Range(49, 53, 8), new Range(0, 11, 42), new Range(42, 0, 7), new Range(57, 7, 4) });
        var waterToLight = new Map("water-to-light", new[] { new Range(88, 18, 7), new Range(18, 25, 70) });
        var lightToTemperature = new Map("light-to-temperature", new[] { new Range(45, 77, 23), new Range(81, 45, 19), new Range(68, 64, 13) });
        var temperatureToHumidity = new Map("temperature-to-humidity", new[] { new Range(0, 69, 1), new Range(1, 0, 69) });
        var humidityToLocation = new Map("humidity-to-location", new[] { new Range(60, 56, 37), new Range(56, 93, 4) });

        var seeds = new[] { 79UL, 14UL, 55UL, 13UL };
        var locations = new List<ulong>(capacity: seeds.Length);

        foreach (var seed in seeds)
        {
            var soil = seedToSoil.MapToDestination(seed);
            var fertilizer = soilToFertilizer.MapToDestination(soil);
            var water = fertilizerToWater.MapToDestination(fertilizer);
            var light = waterToLight.MapToDestination(water);
            var temperature = lightToTemperature.MapToDestination(light);
            var humidity = temperatureToHumidity.MapToDestination(temperature);
            var location = humidityToLocation.MapToDestination(humidity);

            locations.Add(location);
        }

        return locations.Min();
    }

    public override async ValueTask<ulong> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        var inputs = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D05", "input.txt"));
        var seeds = new List<ulong>();
        var maps = new List<Map>();

        Map? currentMap = null;

        foreach (var input in inputs)
        {
            if (string.IsNullOrEmpty(input)) continue;

            if (input.StartsWith("seeds"))
            {
                var seedsRaw = input.Replace("seeds: ", string.Empty).Split(' ', StringSplitOptions.TrimEntries);
                foreach (var seedRaw in seedsRaw)
                {
                    seeds.Add(Convert.ToUInt64(seedRaw));
                }
                continue;
            }

            if (input.Contains("map"))
            {
                currentMap = new Map(input.Split(' ')[0], new List<Range>());
                maps.Add(currentMap);
                continue;
            }

            var values = input.Split(' ', StringSplitOptions.TrimEntries).Select(v => Convert.ToUInt64(v)).ToArray();
            currentMap!.Ranges.Add(new Range(values[0], values[1], values[2]));
        }

        var locations = new List<ulong>(capacity: seeds.Count);
        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var map in maps)
            {
                value = map.MapToDestination(value);
            }

            locations.Add(value);
        }

        return locations.Min();
    }

    public override async ValueTask<ulong> SolveSecondPartAsync(CancellationToken cancellationToken)
    {
        var inputs = await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "2023", "D05", "input.txt"));
        var seeds = new List<Seed>();
        var maps = new List<Map>();

        Map? currentMap = null;

        foreach (var input in inputs)
        {
            if (string.IsNullOrEmpty(input)) continue;

            if (input.StartsWith("seeds"))
            {
                var seedsRaw = input.Replace("seeds: ", string.Empty).Split(' ', StringSplitOptions.TrimEntries).Select(v => Convert.ToUInt64(v)).ToArray();
                for (int i = 0; i <= seedsRaw.Length; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        // end of pair
                        var start = seedsRaw[i - 1];
                        var length = seedsRaw[i];
                        seeds.Add(new Seed(seeds.Count + 1, start, length));
                    }
                }
                continue;
            }

            if (input.Contains("map"))
            {
                currentMap = new Map(input.Split(' ')[0], new List<Range>());
                maps.Add(currentMap);
                continue;
            }

            var values = input.Split(' ', StringSplitOptions.TrimEntries).Select(v => Convert.ToUInt64(v)).ToArray();
            currentMap!.Ranges.Add(new Range(values[0], values[1], values[2]));
        }

        var mutex = new object();
        var locations = new Dictionary<int, ulong>(capacity: seeds.Count);

        Parallel.ForEach(seeds, seed =>
        {
            if (cancellationToken.IsCancellationRequested) return;

            var count = seed.Length - 1;

            for (var j = 0UL; j <= count; j++)
            {
                if (j == 0UL || j % 1_000_000UL == 0)
                {
                    Console.WriteLine(
                        $"Thread {Environment.CurrentManagedThreadId} - {seed.Id} - {j:n} / {count:n}");

                    if (cancellationToken.IsCancellationRequested) break;
                }

                var value = seed.Start + j;
                foreach (var map in maps)
                {
                    value = map.MapToDestination(value);
                }

                var location = locations.GetValueOrDefault(seed.Id);
                if (value < location || location == 0UL) locations[seed.Id] = value;
            }
        });

        return locations.Select(kvp => kvp.Value).Min();
    }
}
