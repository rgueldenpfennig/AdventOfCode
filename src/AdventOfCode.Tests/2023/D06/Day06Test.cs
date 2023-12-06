using AdventOfCode.Y2023.D06;
using static AdventOfCode.Y2023.D06.Day06;

namespace AdventOfCode.Tests.Y2023.D06;

public class Day06Test
{
    [Fact]
    public void ShouldSolveExample()
    {
        var sut = new Day06();
        var result = sut.SolveExample();

        Assert.Equal(288U, result);
    }

    [Fact]
    public async Task ShouldSolveFirstPart()
    {
        var sut = new Day06();
        var result = await sut.SolveFirstPartAsync(CancellationToken.None);

        Assert.Equal(2_756_160U, result);
    }

    [Theory]
    [InlineData(7, 9, 0, 0)]
    [InlineData(7, 9, 1, 6)]
    [InlineData(7, 9, 2, 10)]
    [InlineData(7, 9, 3, 12)]
    [InlineData(7, 9, 4, 12)]
    [InlineData(7, 9, 5, 10)]
    [InlineData(7, 9, 6, 6)]
    [InlineData(7, 9, 7, 0)]
    public void ShouldHandleGetDistanceByCharge(uint time, uint distance, uint chargeTime, uint expectedDistance)
    {
        var sut = new Race(time, distance);
        var result = sut.GetDistanceByCharge(chargeTime);

        Assert.Equal(expectedDistance, result);
    }
}
