using Application.CQRS.Commands.User;
using Application.CQRS.Queries.User;
using Contracts.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignupAsync([FromBody] UserSignupRequest request)
        {
            return Ok(await _sender.Send(new SignUpUserCommand(request)));
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserAuthorizationRequest request)
        {
            return Ok(await _sender.Send(new SignInUserQuery(request)));
        }

        [Authorize]
        [HttpGet("about")]
        public async Task<ActionResult> AboutAsync()
        {
            return Ok(await _sender.Send(new AboutUserQuery()));
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult Authenticate()
        {
            return Ok();
        }
    }
}
