using Application.CQRS.Commands.User;
using Application.CQRS.Queries.User;
using Contracts.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            return Ok(await _sender.Send(new SignUpUserCommand(request)));
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserAuthorizationRequest request)
        {
            return Ok(await _sender.Send(new SignInUserQuery(request)));
        }
    }
}
