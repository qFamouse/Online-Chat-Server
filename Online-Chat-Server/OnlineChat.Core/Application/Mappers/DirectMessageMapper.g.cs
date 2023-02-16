using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Mappers;
using Contracts.Views;
using Contracts.Views.Attachment;
using Contracts.Views.DirectMessage;
using Domain.Entities;

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
        public ChatMessageView MapToChatMessageView(DirectMessage p4)
        {
            return p4 == null ? null : new ChatMessageView()
            {
                Id = p4.Id,
                SenderId = p4.SenderId,
                ReceiverId = p4.ReceiverId,
                Message = p4.Message,
                CreatedAt = p4.CreatedAt
            };
        }
        public ChatMessageDetailView MapToDetailView(DirectMessage p5)
        {
            return p5 == null ? null : new ChatMessageDetailView()
            {
                Id = p5.Id,
                SenderId = p5.SenderId,
                ReceiverId = p5.ReceiverId,
                Message = p5.Message,
                CreatedAt = p5.CreatedAt,
                Attachments = funcMain2(p5.Attachments)
            };
        }
        public IEnumerable<ChatMessageDetailView> MapToDetailView(IEnumerable<DirectMessage> p7)
        {
            return p7 == null ? null : p7.Select<DirectMessage, ChatMessageDetailView>(funcMain3);
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
        
        private List<AttachmentChatView> funcMain2(List<Attachment> p6)
        {
            if (p6 == null)
            {
                return null;
            }
            List<AttachmentChatView> result = new List<AttachmentChatView>(p6.Count);
            
            int i = 0;
            int len = p6.Count;
            
            while (i < len)
            {
                Attachment item = p6[i];
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
        
        private ChatMessageDetailView funcMain3(DirectMessage p8)
        {
            return funcMain4(p8);
        }
        
        private ChatMessageDetailView funcMain4(DirectMessage p9)
        {
            return p9 == null ? null : new ChatMessageDetailView()
            {
                Id = p9.Id,
                SenderId = p9.SenderId,
                ReceiverId = p9.ReceiverId,
                Message = p9.Message,
                CreatedAt = p9.CreatedAt,
                Attachments = funcMain5(p9.Attachments)
            };
        }
        
        private List<AttachmentChatView> funcMain5(List<Attachment> p10)
        {
            if (p10 == null)
            {
                return null;
            }
            List<AttachmentChatView> result = new List<AttachmentChatView>(p10.Count);
            
            int i = 0;
            int len = p10.Count;
            
            while (i < len)
            {
                Attachment item = p10[i];
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