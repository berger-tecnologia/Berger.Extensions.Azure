using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;

namespace Berger.Extensions.Azure.Services.Queue
{
    public class QueueService<T>
    {
        public string Queue { get; set; }
        public string Connection { get; set; }

        public QueueService(string connection, string queue)
        {
            Queue = queue;
            Connection = connection;
        }
        public async Task Add(T model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                await using var client = new ServiceBusClient(Connection);

                ServiceBusSender source = client.CreateSender(Queue);

                ServiceBusMessage message = new ServiceBusMessage(json);

                await source.SendMessageAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}