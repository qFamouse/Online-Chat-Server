using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Application.CQRS.QueryHandlers.User
{
    using Application.CQRS.Queries.User;
    using Application.Entities;
    using Configurations;
    using Contracts.Views;

    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserView>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<UserView> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            return new UserView()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };
        }
    }
}
