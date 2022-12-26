using Application.CQRS.Commands.Participant;
using Application.Interfaces.Repositories;
using Application.Queries;
using FluentValidation;
using Resources;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Participant
{
    internal sealed class AddParticipantByUserIdCommandValidator : AbstractValidator<AddParticipantByUserIdCommand>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IIdentityService _identityService;

        public AddParticipantByUserIdCommandValidator(IConversationRepository conversationRepository, IParticipantRepository participantRepository, IIdentityService identityService)
        {
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

            RuleFor(x => x.UsertId)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync(MustNotBePartisipantOfConversation);

            RuleFor(x => x.ConversationId)
                .NotEmpty()
                .MustAsync(_conversationRepository.ExistsAsync).WithMessage(Messages.NotFound)
                .MustAsync(MustBePartisipantOfConversation).WithMessage(Messages.AccessDenied);
        }

        private async Task<bool> MustBePartisipantOfConversation(int conversationId, CancellationToken cancellationToken)
        {
            var currentUserId = _identityService.GetUserId();
            var participant = await _participantRepository.GetByQueryAsync(new ParticipantQuery()
            {
                ConversationId = conversationId,
                UserId = currentUserId
            });

            return participant != null;
        }

        private async Task<bool> MustNotBePartisipantOfConversation(AddParticipantByUserIdCommand command, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByQueryAsync(new ParticipantQuery()
            {
                ConversationId = command.ConversationId,
                UserId = command.UsertId
            });

            return participant == null;
        }
    }
}
