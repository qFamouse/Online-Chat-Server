using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Repositories;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
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
