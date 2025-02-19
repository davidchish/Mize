using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitManager
{
    public class RateLimit
    {
        public RateLimit(int limit, TimeSpan period)
        {
            Limit = limit;
            Period = period;
        }

        public int Limit { get; }
        public TimeSpan Period { get; }
        public Queue<DateTime> Timestamps { get; } = new Queue<DateTime>();


    }
}
