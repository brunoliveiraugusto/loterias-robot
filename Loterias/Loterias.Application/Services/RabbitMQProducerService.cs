using Loterias.Application.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Loterias.Application.Services
{
    public class RabbitMQProducerService : IMessageProducerService
    {
        public void SendMessage<T>(T message) where T : class
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "messages", durable: false, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "messages", basicProperties: null, body: body);
        }
    }
}
