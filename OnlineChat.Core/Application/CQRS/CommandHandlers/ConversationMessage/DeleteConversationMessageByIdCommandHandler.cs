using Application.CQRS.Commands.Conversation;
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
    internal class DeleteConversationMessageByIdCommandHandler : IRequestHandler<DeleteConversationMessageByIdCommand, Entities.ConversationMessage>
    {
        private readonly IConversationMessagesRepository _conversationMessagesRepository;

        public DeleteConversationMessageByIdCommandHandler(IConversationMessagesRepository conversationMessagesRepository)
        {
            _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
        }

        public Task<Entities.ConversationMessage> Handle(DeleteConversationMessageByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
