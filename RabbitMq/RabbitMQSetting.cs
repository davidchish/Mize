using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMQSetting
    {
        public string Exchange {  get; set; }
        public string Queue { get; set; }
        public string HostName { get; set; }
        public string RoutingKey {  get; set; }
    }
}
