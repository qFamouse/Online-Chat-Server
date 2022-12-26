using Application.CQRS.Commands.Participant;
using Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Participant
{
    internal sealed class RemoveParticipantByUserIdCommandValidator : AbstractValidator<RemoveParticipantByUserIdCommand>
    {
        public RemoveParticipantByUserIdCommandValidator(IParticipantRepository participantRepository)
        {
            // UserId not empty | ConversationID not empty
            // UserId is exists
            // ConversationID is exists
            // User from identity service is owner
        }
    }
}
