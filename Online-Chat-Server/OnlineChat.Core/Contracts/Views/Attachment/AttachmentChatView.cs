using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Views.Attachment
{
    public class AttachmentChatView
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }
}
