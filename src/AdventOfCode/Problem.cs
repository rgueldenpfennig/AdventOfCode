namespace AdventOfCode;

internal abstract class Problem<TResult>
{
    public virtual ValueTask<TResult> SolveFirstPartAsync()
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<TResult> SolveSecondPartAsync()
    {
        throw new NotImplementedException();
    }
}
