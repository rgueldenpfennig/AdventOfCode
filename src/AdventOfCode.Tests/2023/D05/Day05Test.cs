using AdventOfCode.Y2023.D05;
using static AdventOfCode.Y2023.D05.Day05;
using Range = AdventOfCode.Y2023.D05.Day05.Range;

namespace AdventOfCode.Tests.Y2023.D05;

public class Day05Test
{
    [Fact]
    public async Task ShouldSolveFirstPart()
    {
        var sut = new Day05();
        var result = await sut.SolveFirstPartAsync();

        Assert.Equal(662_197_086UL, result);
    }

    [Fact]
    public async Task ShouldSolveSecondPart()
    {
        var sut = new Day05();
        var result = await sut.SolveSecondPartAsync();

        Assert.Equal(662_197_086UL, result);
    }

    [Fact]
    public void ShouldSolveExample()
    {
        var sut = new Day05();
        var result = sut.SolveExample();

        Assert.Equal(35UL, result);
    }

    [Fact]
    public void ShouldHandleMapping()
    {
        var map = new Map("seed-to-soil", new[] { new Range(50, 98, 2), new Range(52, 50, 48) });

        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));

        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));

        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));

        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
        Assert.Equal(0UL, map.MapToDestination(0UL));
    }
}
