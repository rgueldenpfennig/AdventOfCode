namespace AdventOfCode;

internal abstract class Problem<TResult>
{
    public abstract ValueTask<TResult> SolveAsync();
}
