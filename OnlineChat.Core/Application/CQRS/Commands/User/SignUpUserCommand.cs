﻿using MediatR;


namespace Application.CQRS.Commands.User
{
    using Application.Entities;
    using Contracts.Requests.User;

    public class SignUpUserCommand : IRequest<User>
    {
        public User User { get; set; }
        public string Password { get; set; }

        public SignUpUserCommand(UserRegistrationRequest request)
        {
            User = new User()
            {
                UserName = request.UserName,
                Email = request.Email
            };

            Password = request.Password;
        }
    }
}
