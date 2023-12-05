namespace AdventOfCode.Y2023.D05;

internal class Day05 : Problem<int>
{
    internal record Range(int DestinationRangeStart, int SourceRangeStart, int RangeLength)
    {
        public int MaxSourceRange { get; } = SourceRangeStart + (RangeLength - 1);

        public int MaxDestinationRange { get; } = DestinationRangeStart + (RangeLength - 1);
    }

    internal record Map(string Name, IList<Range> Ranges)
    {
        public int MapToDestination(int source)
        {
            var range = GetMatchingRange(source);
            if (range is null)
                return source;

            if (range.SourceRangeStart == source)
                return range.DestinationRangeStart;

            if (source > range.MaxSourceRange)
                return source;

            var sourceDiff = source - range.SourceRangeStart;
            return range.DestinationRangeStart + sourceDiff;
        }

        private Range? GetMatchingRange(int source)
        {
            return Ranges.FirstOrDefault(r => source >= r.SourceRangeStart && source <= r.MaxSourceRange);
        }
    }

    public int SolveExample()
    {
        var seedToSoil = new Map("seed-to-soil", new[] { new Range(50, 98, 2), new Range(52, 50, 48) });
        var soilToFertilizer = new Map("soil-to-fertilizer", new[] { new Range(0, 15, 37), new Range(37, 52, 2), new Range(39, 0, 15) });
        var fertilizerToWater = new Map("fertilizer-to-water", new[] { new Range(49, 53, 8), new Range(0, 11, 42), new Range(42, 0, 7), new Range(57, 7, 4) });
        var waterToLight = new Map("water-to-light", new[] { new Range(88, 18, 7), new Range(18, 25, 70) });
        var lightToTemperature = new Map("light-to-temperature", new[] { new Range(45, 77, 23), new Range(81, 45, 19), new Range(68, 64, 13) });
        var temperatureToHumidity = new Map("temperature-to-humidity", new[] { new Range(0, 69, 1), new Range(1, 0, 69) });
        var humidityToLocation = new Map("humidity-to-location", new[] { new Range(60, 56, 37), new Range(56, 93, 4) });

        var seeds = new[] { 79, 14, 55, 13 };
        var locations = new List<int>(capacity: seeds.Length);

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
}
