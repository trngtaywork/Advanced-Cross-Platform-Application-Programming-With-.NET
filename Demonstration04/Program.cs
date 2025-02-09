using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var tasks = new List<Task<(int TaskId, int Iterations)>>();
        var source = new CancellationTokenSource();
        var token = source.Token;
        int completedIterations = 0;
        int taskIdCounter = 0;

        for (int n = 1; n <= 20; n++)
        {
            int taskId = ++taskIdCounter;
            tasks.Add(Task.Run(() =>
            {
                int iterations = 0;
                try
                {
                    for (int ctr = 1; ctr <= 2_000_000; ctr++)
                    {
                        token.ThrowIfCancellationRequested();
                        iterations++;
                    }
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    Interlocked.Increment(ref completedIterations);
                    if (completedIterations >= 10)
                        source.Cancel();
                }
                return (TaskId: taskId, Iterations: iterations);
            }, token));
        }

        Console.WriteLine("Waiting for the first 10 tasks to complete...\n");

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException)
        {
        }

        Console.WriteLine("Status of tasks:\n");
        Console.WriteLine("{0,-10} {1,-20} {2,-14}", "Task Id", "Status", "Iterations");

        foreach (var t in tasks)
        {
            var result = t.Status == TaskStatus.RanToCompletion ? t.Result : (TaskId: taskIdCounter++, Iterations: -1);
            Console.WriteLine("{0,-10} {1,-20} {2,-14}",
                result.TaskId,
                t.Status,
                t.Status == TaskStatus.RanToCompletion ? result.Iterations.ToString("N0") : "n/a");
        }

        Console.WriteLine("\nProgram finished. Press any key to exit...");
        Console.ReadKey();
    }
}
