using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMqMailingExample.Api.Mediator.Commands;

namespace RabbitMqMailingExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MailingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMailAsync([FromBody] SendMailCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
