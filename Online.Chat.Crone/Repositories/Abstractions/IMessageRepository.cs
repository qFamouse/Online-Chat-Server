using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online.Chat.Crone.Repositories.Abstractions
{
    public interface IMessageRepository
    {
        Task DeleteMessageById(int id);
    }
}
