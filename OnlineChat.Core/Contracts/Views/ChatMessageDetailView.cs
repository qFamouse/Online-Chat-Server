using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Views.Attachment;

namespace Contracts.Views
{
    public class ChatMessageDetailView
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AttachmentChatView> Attachments { get; set; }
    }
}
