using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IDirectMessageRepository : IBaseRepository<DirectMessage>
    {
        Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersId(int senderId, int receiverId);
    }
}
