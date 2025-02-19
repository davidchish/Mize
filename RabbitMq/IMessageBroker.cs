using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public interface IMessageBroker
    {
        public Task SendMessageAsync<T>(T message);

        public void ReciveMessageasync(byte[] message);

    }

}
