using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using FluentValidation;
using Resources;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Commands.Conversation
{
    public sealed class UpdateConversationByIdCommandValidator : AbstractValidator<UpdateConversationByIdCommand>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IIdentityService _identityService;

        public UpdateConversationByIdCommandValidator(IConversationRepository conversationRepository, IIdentityService identityService)
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

            RuleFor(x => x.ConversationId)
                .NotEmpty()
                .MustAsync(_conversationRepository.ExistsAsync).WithMessage(Messages.NotFound)
                .MustAsync(MustBeConversationOwner).WithMessage(Messages.AccessDenied);
        }

        private async Task<bool> MustBeConversationOwner(int conversationId, CancellationToken cancellationToken)
        {
            var currentUserId = _identityService.GetUserId();
            var conversation = await _conversationRepository.GetByIdAsync(conversationId, cancellationToken);
            return conversation.OwnerId == currentUserId;
        }
    }
}
