using Data.Interfaces.Entities;

namespace Data.Entities
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string BlobName { get; set; }
        public string BlobPath { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public int DirectMessageId { get; set; }
        public DirectMessage DirectMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
