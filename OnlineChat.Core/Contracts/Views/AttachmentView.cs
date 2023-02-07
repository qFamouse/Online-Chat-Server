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
        public string BlobName { get; set; }
        public string BlobPath { get; set; }
        public string ContentType { get; set; }
        public int DirectMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
