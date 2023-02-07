using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Views
{
    public class ChatMessageView
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
