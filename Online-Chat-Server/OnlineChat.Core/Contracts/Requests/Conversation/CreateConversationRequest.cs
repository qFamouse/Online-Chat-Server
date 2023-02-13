using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.Conversation
{
    public class CreateConversationRequest
    {
        public string Title { get; set; }
        // TODO: Add logotype (optional)
    }
}
