using AdventOfCode.Y2023.D02;

namespace AdventOfCode.Tests.Y2023.D02;

public class Day02Test
{
    [Fact]
    public void ShouldSolveSample()
    {
        var sut = new Day02();
        var result = sut.SolveSample();

        Assert.Equal(8, result);
    }

    [Fact]
    public async Task ShouldSolveSecondPart()
    {
        var sut = new Day02();
        var result = await sut.SolveAsync();

        Assert.Equal(78_111, result);
    }

    [Fact]
    public async Task ShouldSolveFirstPart()
    {
        var sut = new Day02();
        var result = await sut.SolveFirstPartAsync();

        Assert.Equal(2545, result);
    }

    [Theory]
    [InlineData("Game 1: 20 green, 3 red, 2 blue; 9 red, 16 blue, 18 green; 6 blue, 19 red, 10 green; 12 red, 19 green, 11 blue")]
    [InlineData("Game 100: 1 green, 11 red, 4 blue; 4 green, 1 red; 9 red, 2 blue; 5 blue, 11 red, 9 green")]
    public void ShouldParseGameInput(string input)
    {
        var sut = new Day02();
        var game = sut.ParseGame(input);

        Assert.NotNull(game);
    }
}
