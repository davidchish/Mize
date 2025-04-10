namespace RateLimitManager
{
    public class RateLimiter<TArg>
    {
        private readonly Func<TArg, Task> _action;
        private readonly List<RateLimit> _limits;
        private readonly object _lock = new object();

        public RateLimiter(Func<TArg, Task> action, params (int limit, TimeSpan period)[] limits)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            if (limits == null || limits.Length == 0)
                throw new ArgumentException("At least one rate limit must be provided.", nameof(limits));
            _limits = limits.Select(l => new RateLimit(l.limit, l.period)).ToList();
        }

        public async Task Perform(TArg argument)
        {

            TimeSpan waitTime = TimeSpan.Zero;
            DateTime now = DateTime.UtcNow;

            lock (_lock)
            {

                foreach (var limit in _limits)
                {

                    while (limit.Timestamps.Count > 0 && (now - limit.Timestamps.Peek()) >= limit.Period)
                    {
                        limit.Timestamps.Dequeue();
                    }


                    if (limit.Timestamps.Count >= limit.Limit)
                    {
                        TimeSpan localWait = limit.Period - (now - limit.Timestamps.Peek());
                        if (localWait > waitTime)
                        {
                            waitTime = localWait;
                        }
                    }

                }
            }
            if (waitTime > TimeSpan.Zero)
            {
                await Task.Delay(waitTime);//The delay logic remains safe because timestamps are rechecked only after waiting and just before enqueuing
            }
            DateTime executionTime = DateTime.UtcNow;
            lock (_lock)
            {

                foreach (var limit in _limits)
                {
                    limit.Timestamps.Enqueue(executionTime);
                }
            }
        

            await _action(argument);

        }

    }
}

