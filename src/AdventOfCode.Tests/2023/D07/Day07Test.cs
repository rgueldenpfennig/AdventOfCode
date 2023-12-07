using AdventOfCode.Y2023.D07;

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
}
