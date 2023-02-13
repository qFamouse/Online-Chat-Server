using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Services.Interfaces;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Commands.Conversation
{
    public sealed class DeleteConversationByIdCommandValidator : AbstractValidator<DeleteConversationByIdCommand>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IIdentityService _identityService;

        public DeleteConversationByIdCommandValidator(IConversationRepository conversationRepository, IIdentityService identityService)
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));


            RuleFor(x => x.ConversationId)
                .Cascade(CascadeMode.Stop)
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
