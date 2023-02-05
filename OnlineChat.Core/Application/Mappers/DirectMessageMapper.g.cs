using System.Linq;

namespace Application.Mappers
{
    public partial class DirectMessageMapper : Application.Interfaces.Mappers.IDirectMessageMapper
    {
        public Contracts.Views.DirectMessageView Map(Application.Entities.DirectMessage p1)
        {
            return p1 == null ? null : new Contracts.Views.DirectMessageView()
            {
                Id = p1.Id,
                SenderId = p1.SenderId,
                ReceiverId = p1.ReceiverId,
                Message = p1.Message
            };
        }
        public System.Collections.Generic.IEnumerable<Contracts.Views.ChatMessageDetailView> Map(System.Collections.Generic.IEnumerable<Application.Entities.DirectMessage> p2)
        {
            return p2 == null ? null : p2.Select<Application.Entities.DirectMessage, Contracts.Views.ChatMessageDetailView>(funcMain1);
        }
        
        private Contracts.Views.ChatMessageDetailView funcMain1(Application.Entities.DirectMessage p3)
        {
            return funcMain2(p3);
        }
        
        private Contracts.Views.ChatMessageDetailView funcMain2(Application.Entities.DirectMessage p4)
        {
            return p4 == null ? null : new Contracts.Views.ChatMessageDetailView()
            {
                Id = p4.Id,
                SenderId = p4.SenderId,
                Message = p4.Message,
                Attachments = funcMain3(p4.Attachments)
            };
        }
        
        private System.Collections.Generic.List<Contracts.Views.AttachmentView> funcMain3(System.Collections.Generic.List<Application.Entities.Attachment> p5)
        {
            if (p5 == null)
            {
                return null;
            }
            System.Collections.Generic.List<Contracts.Views.AttachmentView> result = new System.Collections.Generic.List<Contracts.Views.AttachmentView>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                Application.Entities.Attachment item = p5[i];
                result.Add(item == null ? null : new Contracts.Views.AttachmentView()
                {
                    Id = item.Id,
                    OriginalName = item.OriginalName,
                    TimestampName = item.TimestampName,
                    Path = item.Path,
                    DirectMessageId = item.DirectMessageId,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                });
                i++;
            }
            return result;
            
        }
    }
}