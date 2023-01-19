using Application.CQRS.Commands.DirectMessage;
using Application.CQRS.Queries.DirectMessage;
using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectMessagesController : ControllerBase
    {
        private readonly ISender _sender;

        public DirectMessagesController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost]
        public async Task<ActionResult> SendDirectMessageByReceiverIdAsync([FromBody] SendDirectMessageByReceiverIdRequest request)
        {
            return Ok(await _sender.Send(new SendDirectMessageByReceiverIdCommand(request)));
        }

        [HttpGet]
        public async Task<ActionResult> GetDirectChatByReceiverIdAsync([FromQuery(Name = "id")] int id)
        {
            return Ok(await _sender.Send(new GetDirectChatByReceiverIdQuery(id)));
        }

        [HttpGet("interlocutors")]
        public async Task<ActionResult> GetInterlocutorsAsync()
        {
            return Ok(await _sender.Send(new GetInterlocutorsByUserIdQuery()));
        }
    }
}
