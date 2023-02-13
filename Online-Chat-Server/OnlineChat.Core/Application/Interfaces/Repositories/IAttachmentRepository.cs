using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {
        Task<Attachment> GetDetailByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
