using AdventOfCode.Y2023.D05;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += CancelKeyPress;

void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    cts.Cancel();
}

var problem = new Day05();

try
{
    var result = await problem.SolveSecondPartAsync(cts.Token);
    Console.WriteLine(result);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation cancelled");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e}");
}
