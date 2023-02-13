using Application.CQRS.Commands.Conversation;
using Application.CQRS.Queries.Conversation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Queries.Conversation
{
    public sealed class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
    {
        public GetConversationByIdQueryValidator()
        {
            // Conversation is exists
        }
    }
}
