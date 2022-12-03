using Application.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    // ConversationUser
    public class Participant : IEntity
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        // TODO: Add created_at
        // TODO: Add updated_at

    }
}
