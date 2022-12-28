using Application.CQRS.Commands.ConversationMessage;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers.ConversationMessage
{
    internal class SendConversationMessageByConversationIdCommandHandler : IRequestHandler<SendConversationMessageByConversationIdCommand, Entities.ConversationMessage>
    {
        private readonly IConversationMessagesRepository _conversationMessagesRepository;

        public SendConversationMessageByConversationIdCommandHandler(IConversationMessagesRepository conversationMessagesRepository)
        {
            _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
        }

        public Task<Entities.ConversationMessage> Handle(SendConversationMessageByConversationIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
