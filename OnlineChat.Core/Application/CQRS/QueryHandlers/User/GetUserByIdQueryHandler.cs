﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application.CQRS.Queries.User;
using Contracts.Views;

namespace Application.CQRS.QueryHandlers.User
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserView>
    {
        private readonly UserManager<Entities.User> _userManager;

        public GetUserByIdQueryHandler(UserManager<Entities.User> userManager)
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
