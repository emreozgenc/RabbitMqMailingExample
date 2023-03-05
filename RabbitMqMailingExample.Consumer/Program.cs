using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin",
        };

        Console.WriteLine();

        var channel = connectionFactory.CreateConnection().CreateModel();

        channel.QueueDeclare("mailing.send", false, false, false, null);
        channel.ExchangeDeclare("mailing", ExchangeType.Direct, false, false, null);
        channel.QueueBind("mailing.send", "mailing", "mailing.send");

        var eventingBasicConsumer = new EventingBasicConsumer(channel);
        eventingBasicConsumer.Received += (sender, e) =>
        {
            string json = Encoding.UTF8.GetString(e.Body.ToArray());
            var mailing = JsonSerializer.Deserialize<Mailing>(json);
            Console.WriteLine(mailing.Message);
        };

        channel.BasicConsume("mailing.send", true, eventingBasicConsumer);

        Console.ReadLine();

    }

    private class Mailing
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; }
    }
}