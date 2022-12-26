using Application.CQRS.Commands.Conversation;
using Application.CQRS.Commands.DirectMessage;
using Application.CQRS.Commands.Participant;
using Contracts.Requests.Conversation;
using Contracts.Requests.DirectMessage;
using Contracts.Requests.Participant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ISender _sender;

        public ParticipantsController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> AddPartisipantAsync([FromBody] AddParticipantByUserIdRequest request)
        {
            return Ok(await _sender.Send(new AddParticipantByUserIdCommand(request)));
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult> RemovePartisipantAsync(RemoveParticipantByUserIdRequest request)
        {
            return Ok(await _sender.Send(new RemoveParticipantByUserIdCommand(request)));
        }
    }
}
