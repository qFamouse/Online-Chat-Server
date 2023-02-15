using Application.CQRS.Commands.Participants;
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
        public async Task<ActionResult> AddParticipantAsync([FromBody] AddParticipantByUserIdRequest request)
        {
            return Ok(await _sender.Send(new AddParticipantByUserIdCommand(request)));
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult> RemoveParticipantAsync(RemoveParticipantByUserIdRequest request)
        {
            return Ok(await _sender.Send(new RemoveParticipantByUserIdCommand(request)));
        }
    }
}
