using AdventOfCode.Y2023.D03;

namespace AdventOfCode.Tests.Y2023.D03;

public class Day03Test
{
    [Fact]
    public void ShouldSolveSample()
    {
        var result = Day03.SolveSample();
        Assert.Equal(4361, result);
    }
}
