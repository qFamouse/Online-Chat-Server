using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Contracts.Views;
using Mapster;

namespace Application.Interfaces.Mappers
{
    [Mapper]
    public interface IAttachmentMapper
    {
        AttachmentView MapToView(Attachment message);   
        IEnumerable<AttachmentView> MapToView(IEnumerable<Attachment> message);

        AttachmentDetailView MapToDetailView(Attachment message);
        IEnumerable<AttachmentDetailView> MapToDetailView(IEnumerable<Attachment> message);
    }
}
