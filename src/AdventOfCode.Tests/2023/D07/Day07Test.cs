using AdventOfCode.Y2023.D07;
using static AdventOfCode.Y2023.D07.Day07;

namespace AdventOfCode.Tests.Y2023.D07;

public class Day07Test
{
    [Fact]
    public void ShouldSolveExample()
    {
        var sut = new Day07();
        var result = sut.SolveExample();

        Assert.Equal(6440, result);
    }

    [Fact]
    public void ShouldSolveExamplePartTwo()
    {
        var sut = new Day07();
        Part2Rules = true;
        var result = sut.SolveExample();
        Part2Rules = false;

        Assert.Equal(5905, result);
    }

    [Fact]
    public async Task ShouldSolveFirstPart()
    {
        var sut = new Day07();
        var result = await sut.SolveFirstPartAsync(CancellationToken.None);

        Assert.Equal(253_603_890, result);
    }

    [Fact]
    public async Task ShouldSolveSecondPart()
    {
        var sut = new Day07();
        Part2Rules = true;
        var result = await sut.SolveSecondPartAsync(CancellationToken.None);
        Part2Rules = false;

        Assert.Equal(253_630_098, result);
    }

    [Theory]
    [InlineData("32T4K 1", HandType.HighCard)]
    [InlineData("32T3K 1", HandType.OnePair)]
    [InlineData("3223K 1", HandType.TwoPair)]
    [InlineData("3323K 1", HandType.ThreeOfAKind)]
    [InlineData("33223 1", HandType.FullHouse)]
    [InlineData("33233 1", HandType.FourOfAKind)]
    [InlineData("33333 1", HandType.FiveOfAKind)]
    public void ShouldSolveHandType(string input, HandType expected)
    {
        var sut = ParseHand(input);
        Assert.Equal(expected, sut.Type);
    }

    [Theory]
    [InlineData("32T4K 1", HandType.HighCard)]
    [InlineData("32TJK 1", HandType.OnePair)]
    [InlineData("322JK 1", HandType.ThreeOfAKind)]
    [InlineData("332JK 1", HandType.ThreeOfAKind)]
    [InlineData("3344J 1", HandType.FullHouse)]
    [InlineData("332J3 1", HandType.FourOfAKind)]
    [InlineData("337J3 1", HandType.FourOfAKind)]
    [InlineData("33J33 1", HandType.FiveOfAKind)]
    ///
    [InlineData("32T3K 1", HandType.OnePair)]
    [InlineData("T55J5 1", HandType.FourOfAKind)]
    [InlineData("KK677 1", HandType.TwoPair)]
    [InlineData("KTJJT 1", HandType.FourOfAKind)]
    [InlineData("QQQJA 1", HandType.FourOfAKind)]
    public void ShouldSolveHandTypeWithJoker(string input, HandType expected)
    {
        Part2Rules = true;
        var sut = ParseHand(input);
        Part2Rules = false;
        Assert.Equal(expected, sut.Type);
    }
}
