using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task DeleteMessageByIdAsync(int id);
    }
}
