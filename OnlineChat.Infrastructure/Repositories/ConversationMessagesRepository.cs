using Application.Entities;
using Application.Interfaces.Repositories;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ConversationMessagesRepository : BaseRepository<ConversationMessage>, IConversationMessagesRepository
    {
        public ConversationMessagesRepository(OnlineChatContext context) : base(context) { }
    }
}
