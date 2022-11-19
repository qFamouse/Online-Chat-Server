using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.Entities
{
    public class DirectMessage : IEntity
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public User Sender { get; set; }
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string Message { get; set; }
        // TODO: Add created_at
        // TODO: Add updated_at
        // TODO: Add attachment
    }
}
