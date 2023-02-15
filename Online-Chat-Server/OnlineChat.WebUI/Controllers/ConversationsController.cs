using Application.CQRS.Commands.Conversations;
using Contracts.Requests.Conversation;
using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly ISender _sender;

        public ConversationsController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> CreateConversationAsync([FromBody] CreateConversationRequest request)
        {
            return Ok(await _sender.Send(new CreateConversationCommand(request)));
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConversationAsync(int id)
        {
            return Ok(await _sender.Send(new DeleteConversationByIdCommand(id)));
        }

        [Authorize(Roles = "User")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateConversationAsync(int id, [FromBody] UpdateConversationByIdRequest request)
        {
            return Ok(await _sender.Send(new UpdateConversationByIdCommand(id, request)));
        }
    }
}
