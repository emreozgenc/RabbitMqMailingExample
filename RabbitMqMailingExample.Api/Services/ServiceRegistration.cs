using RabbitMqMailingExample.Api.Mediator.Commands;
using RabbitMqMailingExample.Api.Services.Abstract;
using RabbitMqMailingExample.Api.Services.Concrete;
using System.Collections.Generic;

namespace RabbitMqMailingExample.Api.Services
{
    public static class ServiceRegistration
    {
        public static void AddRabbitMqService(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }

        public static IEnumerable<SendMailCommand> Test(this IEnumerable<SendMailCommand> test)
        {
            foreach(var item in test)
            {
                item.Message = "abc";
                yield return item;
            }
        }
    }
}
