namespace AdventOfCode;

public abstract class Problem<TResult>
{
    public virtual ValueTask<TResult> SolveFirstPartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public virtual ValueTask<TResult> SolveSecondPartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
