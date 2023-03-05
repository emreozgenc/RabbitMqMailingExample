using MediatR;
using RabbitMqMailingExample.Api.Services;
using RabbitMqMailingExample.Api.Services.Abstract;
using System.Text.Json;

namespace RabbitMqMailingExample.Api.Mediator.Commands
{
    public class SendMailCommand : IRequest<bool>
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; }
    }

    public class SendMailCommandHandler : IRequestHandler<SendMailCommand, bool>
    {
        private readonly IRabbitMqService _rabbitMqService;

        public SendMailCommandHandler(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task<bool> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            string json = JsonSerializer.Serialize(request);
            _rabbitMqService.PublishMessage(json, "mailing.send", "mailing");

            return await Task.FromResult(true);
        }
    }
}
