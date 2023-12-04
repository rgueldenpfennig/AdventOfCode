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
}
