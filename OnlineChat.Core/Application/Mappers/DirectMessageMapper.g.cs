using System.Collections.Generic;
using System.Linq;
using Application.Entities;
using Application.Interfaces.Mappers;
using Contracts.Views;
using Contracts.Views.Attachment;
using Contracts.Views.DirectMessage;

namespace Application.Mappers
{
    public partial class DirectMessageMapper : IDirectMessageMapper
    {
        public DirectMessageView MapToView(DirectMessage p1)
        {
            return p1 == null ? null : new DirectMessageView()
            {
                Id = p1.Id,
                SenderId = p1.SenderId,
                ReceiverId = p1.ReceiverId,
                Message = p1.Message,
                CreatedAt = p1.CreatedAt
            };
        }
        public IEnumerable<DirectMessageView> MapToView(IEnumerable<DirectMessage> p2)
        {
            return p2 == null ? null : p2.Select<DirectMessage, DirectMessageView>(funcMain1);
        }
        public ChatMessageDetailView MapToDetailView(DirectMessage p4)
        {
            return p4 == null ? null : new ChatMessageDetailView()
            {
                Id = p4.Id,
                SenderId = p4.SenderId,
                Message = p4.Message,
                CreatedAt = p4.CreatedAt,
                Attachments = funcMain2(p4.Attachments)
            };
        }
        public IEnumerable<ChatMessageDetailView> MapToDetailView(IEnumerable<DirectMessage> p6)
        {
            return p6 == null ? null : p6.Select<DirectMessage, ChatMessageDetailView>(funcMain3);
        }
        
        private DirectMessageView funcMain1(DirectMessage p3)
        {
            return p3 == null ? null : new DirectMessageView()
            {
                Id = p3.Id,
                SenderId = p3.SenderId,
                ReceiverId = p3.ReceiverId,
                Message = p3.Message,
                CreatedAt = p3.CreatedAt
            };
        }
        
        private List<AttachmentChatView> funcMain2(List<Attachment> p5)
        {
            if (p5 == null)
            {
                return null;
            }
            List<AttachmentChatView> result = new List<AttachmentChatView>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                Attachment item = p5[i];
                result.Add(item == null ? null : new AttachmentChatView()
                {
                    Id = item.Id,
                    OriginalName = item.OriginalName,
                    ContentType = item.ContentType,
                    ContentLength = item.ContentLength
                });
                i++;
            }
            return result;
            
        }
        
        private ChatMessageDetailView funcMain3(DirectMessage p7)
        {
            return funcMain4(p7);
        }
        
        private ChatMessageDetailView funcMain4(DirectMessage p8)
        {
            return p8 == null ? null : new ChatMessageDetailView()
            {
                Id = p8.Id,
                SenderId = p8.SenderId,
                Message = p8.Message,
                CreatedAt = p8.CreatedAt,
                Attachments = funcMain5(p8.Attachments)
            };
        }
        
        private List<AttachmentChatView> funcMain5(List<Attachment> p9)
        {
            if (p9 == null)
            {
                return null;
            }
            List<AttachmentChatView> result = new List<AttachmentChatView>(p9.Count);
            
            int i = 0;
            int len = p9.Count;
            
            while (i < len)
            {
                Attachment item = p9[i];
                result.Add(item == null ? null : new AttachmentChatView()
                {
                    Id = item.Id,
                    OriginalName = item.OriginalName,
                    ContentType = item.ContentType,
                    ContentLength = item.ContentLength
                });
                i++;
            }
            return result;
            
        }
    }
}