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
    internal sealed class AddParticipantByUserIdCommandValidator : AbstractValidator<AddParticipantByUserIdCommand>
    {
        public AddParticipantByUserIdCommandValidator(IParticipantRepository participantRepository)
        {
            // UserId not empty | ConversationID not empty
            // User from identity service is participant
            // UserId is exists
            // ConversationID is exists
            // UserId is not member ConversationId
        }
    }
}
