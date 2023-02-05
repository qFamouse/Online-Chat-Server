using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Views
{
    public class ChatMessageDetailView
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public List<AttachmentView> Attachments { get; set; }
    }
}
