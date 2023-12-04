using AdventOfCode.Y2023.D04;

namespace AdventOfCode.Tests.Y2023.D04;

public class Day04Test
{
    [Fact]
    public async Task ShouldSolveFirstPart()
    {
        var sut = new Day04();
        var result = await sut.SolveFirstPartAsync();

        Assert.Equal(32_609, result);
    }

    [Fact]
    public async Task ShouldSolveSecondPart()
    {
        var sut = new Day04();
        var result = await sut.SolveSecondPartAsync();

        Assert.Equal(14_624_680, result);
    }
}
