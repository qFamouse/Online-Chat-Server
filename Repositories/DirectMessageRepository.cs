using Application.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DirectMessageRepository : BaseRepository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(OnlineChatContext context) : base(context) { }

        public async Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersId(int senderId, int receiverId)
        {
            var fromSender = DbContext.DirectMessages.Where(dm => dm.SenderId == senderId && dm.ReceiverId == receiverId);
            var fromReceiver = DbContext.DirectMessages.Where(dm => dm.SenderId == receiverId && dm.ReceiverId == senderId);

            return await fromSender.Union(fromReceiver).ToListAsync();
        }
    }
}
