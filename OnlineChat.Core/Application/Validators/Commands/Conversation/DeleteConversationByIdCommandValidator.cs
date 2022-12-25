using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using FluentValidation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Commands.Conversation
{
    public sealed class DeleteConversationByIdCommandValidator : AbstractValidator<DeleteConversationByIdCommand>
    {
        public DeleteConversationByIdCommandValidator(IConversationRepository conversationRepository, IIdentityService identityService)
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty()
                // Exists in database
                .MustAsync(async (id, cancellation) => await conversationRepository.GetByIdAsync(id) != null);
            // User is owner of conversation
        }
    }
}
