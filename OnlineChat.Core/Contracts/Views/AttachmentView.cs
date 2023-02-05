using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Views
{
    public class AttachmentView
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string TimestampName { get; set; }
        public string Path { get; set; }
        public int DirectMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
