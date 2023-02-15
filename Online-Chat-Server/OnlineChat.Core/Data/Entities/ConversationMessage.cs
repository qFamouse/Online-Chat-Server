using Data.Interfaces.Entities;

namespace Data.Entities
{
    public class ConversationMessage : IEntity
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
        // TODO: Add attachment
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
