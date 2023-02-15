using Data.Entities;

namespace Application.Interfaces.Repositories;

public interface IAttachmentRepository : IBaseRepository<Attachment>
{
    Task<Attachment> GetDetailByIdAsync(int id, CancellationToken cancellationToken = default);
}