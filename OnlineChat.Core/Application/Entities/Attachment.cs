using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Entities;

namespace Application.Entities
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string TimestampName { get; set; }
        public string Path { get; set; }
        public int DirectMessageId { get; set; }
        public DirectMessage DirectMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
