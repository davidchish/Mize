using RateLimitManager;

public class Program
{
    public static async Task Main(string[] args)
    {

        Func<int, Task> action = async (arg) =>
        {
            Console.WriteLine($"Action executed for argument: {arg} at {DateTime.UtcNow:HH:mm:ss.fff}");
            await Task.Delay(100); // Simulate some work
        };

        var rateLimiter = new RateLimiter<int>(action,
            (5, TimeSpan.FromSeconds(1)),
            (10, TimeSpan.FromSeconds(3))
        );


        var tasks = new List<Task>();
        for (int i = 1; i <= 20; i++)
        {
            int arg = i;
            tasks.Add(Task.Run(() => rateLimiter.Perform(arg)));
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All actions executed.");
    }
}