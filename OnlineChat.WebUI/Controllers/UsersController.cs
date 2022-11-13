using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Core.Commands.Users;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Interfaces.Services;
using OnlineChat.Core.Queries;
using OnlineChat.Core.Queries.Users;
using OnlineChat.Core.Requests.User;

namespace OnlineChat.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            //_userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            return Ok(await _sender.Send(new RegistrationCommand(request)));
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserAuthorizationRequest request)
        {
            return Ok(await _sender.Send(new AuthorizationQuery(request)));
        }
    }
}
