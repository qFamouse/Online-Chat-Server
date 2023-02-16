using System.Net;
using Data.Entities;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using Shared;

namespace Repositories
{
    public class AttachmentRepository : BaseRepository<Attachment> , IAttachmentRepository
    {
        public AttachmentRepository(OnlineChatContext context) : base(context) { }

        public async Task<Attachment> GetDetailByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Attachments
                .Include(a => a.DirectMessage)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken) 
                   ?? throw new ProblemDetailsException((int)HttpStatusCode.NotFound);
        }
    }
}
