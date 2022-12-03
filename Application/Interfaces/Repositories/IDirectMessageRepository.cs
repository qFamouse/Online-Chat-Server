using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDirectMessageRepository : IBaseRepository<DirectMessage>
    {
        Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersId(int senderId, int receiverId);
    }
}
