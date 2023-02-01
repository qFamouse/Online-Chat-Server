using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Repositories;
using Shared;

namespace Repositories
{
    public class AttachmentRepository : BaseRepository<Attachment> , IAttachmentRepository
    {
        public AttachmentRepository(OnlineChatContext context) : base(context) { }
    }
}
