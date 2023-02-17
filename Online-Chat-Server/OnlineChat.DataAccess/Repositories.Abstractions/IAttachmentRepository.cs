using Domain.Entities;

namespace Repositories.Abstractions;

public interface IAttachmentRepository : IBaseRepository<Attachment>
{
    Task<Attachment> GetDetailByIdAsync(int id, CancellationToken cancellationToken = default);
}