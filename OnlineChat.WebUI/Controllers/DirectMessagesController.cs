using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Core.Requests.DirectMessage;
using OnlineChat.Core.Requests.User;
using OnlineChat.Core.SQRS.Commands.DirectMessage;
using OnlineChat.Core.SQRS.Commands.User;
using OnlineChat.Core.SQRS.Queries.DirectMessage;

namespace OnlineChat.WebUI.Controllers
{
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
    }
}
