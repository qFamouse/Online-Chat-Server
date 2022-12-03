using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                Name = request.Name,
                UserName = request.Email,
                Email = request.Email
            };

            Password = request.Password;
        }
    }
}
