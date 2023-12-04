using AdventOfCode.Y2023.D01;

namespace AdventOfCode.Tests.Y2023.D01;

public class Day01Test
{
    [Fact]
    public async Task ShouldSolveProblem()
    {
        var sut = new Day01();
        var result = await sut.SolveSecondPartAsync();

        Assert.Equal(55413, result);
    }

    [Theory]
    [InlineData("51591twosix4dhsxvgghxq", 54)]
    [InlineData("425nine", 49)]
    [InlineData("5sjnnfivefourzxxfpfivenine7five", 55)]
    [InlineData("7hjmmxhdnine8", 78)]
    [InlineData("26seven1", 21)]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    public void ShouldCalculateResult(string input, int expectedResult)
    {
        var sut = new Day01();
        var result = sut.CalculateResult(input);

        Assert.Equal(expectedResult, result);
    }
}
