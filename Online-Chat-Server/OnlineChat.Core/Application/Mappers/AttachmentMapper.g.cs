using System.Collections.Generic;
using System.Linq;
using Application.Mappers.Abstractions;
using Contracts.Views.Attachment;
using Contracts.Views.DirectMessage;
using Domain.Entities;

namespace Application.Mappers
{
    public partial class AttachmentMapper : IAttachmentMapper
    {
        public AttachmentChatView MapToChatView(Attachment p1)
        {
            return p1 == null ? null : new AttachmentChatView()
            {
                Id = p1.Id,
                OriginalName = p1.OriginalName,
                ContentType = p1.ContentType,
                ContentLength = p1.ContentLength
            };
        }
        public IEnumerable<AttachmentChatView> MapToChatView(IEnumerable<Attachment> p2)
        {
            return p2 == null ? null : p2.Select<Attachment, AttachmentChatView>(funcMain1);
        }
        public AttachmentView MapToView(Attachment p4)
        {
            return p4 == null ? null : new AttachmentView()
            {
                Id = p4.Id,
                OriginalName = p4.OriginalName,
                BlobName = p4.BlobName,
                BlobPath = p4.BlobPath,
                ContentType = p4.ContentType,
                DirectMessageId = p4.DirectMessageId,
                CreatedAt = p4.CreatedAt,
                UpdatedAt = p4.UpdatedAt
            };
        }
        public IEnumerable<AttachmentView> MapToView(IEnumerable<Attachment> p5)
        {
            return p5 == null ? null : p5.Select<Attachment, AttachmentView>(funcMain2);
        }
        public AttachmentDetailView MapToDetailView(Attachment p7)
        {
            return p7 == null ? null : new AttachmentDetailView()
            {
                Id = p7.Id,
                OriginalName = p7.OriginalName,
                BlobName = p7.BlobName,
                BlobPath = p7.BlobPath,
                ContentType = p7.ContentType,
                DirectMessageId = p7.DirectMessageId,
                DirectMessage = p7.DirectMessage == null ? null : new DirectMessageView()
                {
                    Id = p7.DirectMessage.Id,
                    SenderId = p7.DirectMessage.SenderId,
                    ReceiverId = p7.DirectMessage.ReceiverId,
                    Message = p7.DirectMessage.Message,
                    CreatedAt = p7.DirectMessage.CreatedAt
                },
                CreatedAt = p7.CreatedAt,
                UpdatedAt = p7.UpdatedAt
            };
        }
        public IEnumerable<AttachmentDetailView> MapToDetailView(IEnumerable<Attachment> p8)
        {
            return p8 == null ? null : p8.Select<Attachment, AttachmentDetailView>(funcMain3);
        }
        
        private AttachmentChatView funcMain1(Attachment p3)
        {
            return p3 == null ? null : new AttachmentChatView()
            {
                Id = p3.Id,
                OriginalName = p3.OriginalName,
                ContentType = p3.ContentType,
                ContentLength = p3.ContentLength
            };
        }
        
        private AttachmentView funcMain2(Attachment p6)
        {
            return p6 == null ? null : new AttachmentView()
            {
                Id = p6.Id,
                OriginalName = p6.OriginalName,
                BlobName = p6.BlobName,
                BlobPath = p6.BlobPath,
                ContentType = p6.ContentType,
                DirectMessageId = p6.DirectMessageId,
                CreatedAt = p6.CreatedAt,
                UpdatedAt = p6.UpdatedAt
            };
        }
        
        private AttachmentDetailView funcMain3(Attachment p9)
        {
            return p9 == null ? null : new AttachmentDetailView()
            {
                Id = p9.Id,
                OriginalName = p9.OriginalName,
                BlobName = p9.BlobName,
                BlobPath = p9.BlobPath,
                ContentType = p9.ContentType,
                DirectMessageId = p9.DirectMessageId,
                DirectMessage = p9.DirectMessage == null ? null : new DirectMessageView()
                {
                    Id = p9.DirectMessage.Id,
                    SenderId = p9.DirectMessage.SenderId,
                    ReceiverId = p9.DirectMessage.ReceiverId,
                    Message = p9.DirectMessage.Message,
                    CreatedAt = p9.DirectMessage.CreatedAt
                },
                CreatedAt = p9.CreatedAt,
                UpdatedAt = p9.UpdatedAt
            };
        }
    }
}