using RabbitMQ.Client;
using RabbitMqMailingExample.Api.Services.Abstract;
using System.Text;

namespace RabbitMqMailingExample.Api.Services.Concrete
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnectionFactory _connectionFactory;
        private IModel _channel;

        public RabbitMqService(IConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMq:HostName"],
                UserName = configuration["RabbitMq:UserName"],
                Password = configuration["RabbitMq:Password"],
            };
        }

        public void PublishMessage(string body, string queue, string exchange = "")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            PublishMessage(bytes, queue, exchange);

        }

        private void OpenConnection()
        {
            _channel ??= _connectionFactory.CreateConnection().CreateModel();
        }

        public void PublishMessage(byte[] body, string queue, string exchange = "")
        {
            OpenConnection();
            DeclareQueueAndExchange(queue, exchange);

            _channel.BasicPublish(exchange: exchange, routingKey: queue, basicProperties: null, body: body);
        }

        private void DeclareQueueAndExchange(string queue, string exchange, string type = ExchangeType.Direct)
        {
            _channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false);

            if (!string.IsNullOrEmpty(exchange))
            {
                _channel.ExchangeDeclare(
                        exchange: exchange,
                        type: type,
                        durable: false); ;
                _channel.QueueBind(queue: queue, exchange: exchange, routingKey: queue);
            }


        }
    }
}
