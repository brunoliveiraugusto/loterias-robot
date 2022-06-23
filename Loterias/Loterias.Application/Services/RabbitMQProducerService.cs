using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Loterias.Application.Services
{
    public class RabbitMQProducerService : IMessageProducerService
    {
        private readonly RabbitMqSettings _config;
        private readonly ConnectionFactory _factory;

        public RabbitMQProducerService(IOptions<RabbitMqSettings> options)
        {
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _config.Host
            };
        }

        public void SendMessage<T>(T message) where T : class
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _config.Queue, durable: false, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _config.Queue, basicProperties: null, body: body);
        }
    }
}
