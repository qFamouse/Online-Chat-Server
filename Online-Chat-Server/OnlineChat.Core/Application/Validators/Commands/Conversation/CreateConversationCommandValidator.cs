using Application.CQRS.Commands.Conversation;
using Application.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Commands.Conversation
{
    public sealed class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
    {
        public CreateConversationCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
