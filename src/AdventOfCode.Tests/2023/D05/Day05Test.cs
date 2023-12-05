using AdventOfCode.Y2023.D05;
using static AdventOfCode.Y2023.D05.Day05;
using Range = AdventOfCode.Y2023.D05.Day05.Range;

namespace AdventOfCode.Tests.Y2023.D05;

public class Day05Test
{
    //[Fact]
    //public async Task ShouldSolveFirstPart()
    //{
    //    var sut = new Day04();
    //    var result = await sut.SolveFirstPartAsync();

    //    Assert.Equal(32_609, result);
    //}

    //[Fact]
    //public async Task ShouldSolveSecondPart()
    //{
    //    var sut = new Day04();
    //    var result = await sut.SolveSecondPartAsync();

    //    Assert.Equal(14_624_680, result);
    //}

    [Fact]
    public void ShouldSolveExample()
    {
        var sut = new Day05();
        var result = sut.SolveExample();

        Assert.Equal(35, result);
    }

    [Fact]
    public void ShouldHandleMapping()
    {
        var map = new Map("seed-to-soil", new[] { new Range(50, 98, 2), new Range(52, 50, 48) });

        Assert.Equal(0, map.MapToDestination(0));
        Assert.Equal(1, map.MapToDestination(1));

        Assert.Equal(48, map.MapToDestination(48));
        Assert.Equal(49, map.MapToDestination(49));
        Assert.Equal(52, map.MapToDestination(50));
        Assert.Equal(53, map.MapToDestination(51));

        Assert.Equal(98, map.MapToDestination(96));
        Assert.Equal(99, map.MapToDestination(97));
        Assert.Equal(50, map.MapToDestination(98));
        Assert.Equal(51, map.MapToDestination(99));

        Assert.Equal(81, map.MapToDestination(79));
        Assert.Equal(14, map.MapToDestination(14));
        Assert.Equal(57, map.MapToDestination(55));
        Assert.Equal(13, map.MapToDestination(13));
    }
}
