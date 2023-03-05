namespace RabbitMqMailingExample.Api.Services.Abstract
{
    public interface IRabbitMqService
    {
        void PublishMessage(string body, string queue, string exchange = "");
        void PublishMessage(byte[] body, string queue, string exchange = "");
    }
}
