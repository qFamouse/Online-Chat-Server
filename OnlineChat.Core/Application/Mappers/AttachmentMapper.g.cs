using System.Collections.Generic;
using System.Linq;
using Application.Entities;
using Application.Interfaces.Mappers;
using Contracts.Views;

namespace Application.Mappers
{
    public partial class AttachmentMapper : IAttachmentMapper
    {
        public AttachmentView MapToView(Attachment p1)
        {
            return p1 == null ? null : new AttachmentView()
            {
                Id = p1.Id,
                OriginalName = p1.OriginalName,
                BlobName = p1.BlobName,
                BlobPath = p1.BlobPath,
                ContentType = p1.ContentType,
                DirectMessageId = p1.DirectMessageId,
                CreatedAt = p1.CreatedAt,
                UpdatedAt = p1.UpdatedAt
            };
        }
        public IEnumerable<AttachmentView> MapToView(IEnumerable<Attachment> p2)
        {
            return p2 == null ? null : p2.Select<Attachment, AttachmentView>(funcMain1);
        }
        public AttachmentDetailView MapToDetailView(Attachment p4)
        {
            return p4 == null ? null : new AttachmentDetailView()
            {
                Id = p4.Id,
                OriginalName = p4.OriginalName,
                BlobName = p4.BlobName,
                BlobPath = p4.BlobPath,
                ContentType = p4.ContentType,
                DirectMessageId = p4.DirectMessageId,
                DirectMessage = p4.DirectMessage == null ? null : new DirectMessageView()
                {
                    Id = p4.DirectMessage.Id,
                    SenderId = p4.DirectMessage.SenderId,
                    ReceiverId = p4.DirectMessage.ReceiverId,
                    Message = p4.DirectMessage.Message
                },
                CreatedAt = p4.CreatedAt,
                UpdatedAt = p4.UpdatedAt
            };
        }
        public IEnumerable<AttachmentDetailView> MapToDetailView(IEnumerable<Attachment> p5)
        {
            return p5 == null ? null : p5.Select<Attachment, AttachmentDetailView>(funcMain2);
        }
        
        private AttachmentView funcMain1(Attachment p3)
        {
            return p3 == null ? null : new AttachmentView()
            {
                Id = p3.Id,
                OriginalName = p3.OriginalName,
                BlobName = p3.BlobName,
                BlobPath = p3.BlobPath,
                ContentType = p3.ContentType,
                DirectMessageId = p3.DirectMessageId,
                CreatedAt = p3.CreatedAt,
                UpdatedAt = p3.UpdatedAt
            };
        }
        
        private AttachmentDetailView funcMain2(Attachment p6)
        {
            return p6 == null ? null : new AttachmentDetailView()
            {
                Id = p6.Id,
                OriginalName = p6.OriginalName,
                BlobName = p6.BlobName,
                BlobPath = p6.BlobPath,
                ContentType = p6.ContentType,
                DirectMessageId = p6.DirectMessageId,
                DirectMessage = p6.DirectMessage == null ? null : new DirectMessageView()
                {
                    Id = p6.DirectMessage.Id,
                    SenderId = p6.DirectMessage.SenderId,
                    ReceiverId = p6.DirectMessage.ReceiverId,
                    Message = p6.DirectMessage.Message
                },
                CreatedAt = p6.CreatedAt,
                UpdatedAt = p6.UpdatedAt
            };
        }
    }
}