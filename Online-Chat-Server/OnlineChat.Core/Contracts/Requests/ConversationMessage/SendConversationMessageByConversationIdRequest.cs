using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.ConversationMessage
{
    public class SendConversationMessageByConversationIdRequest
    {
        public int ConversationId { get; set; }
        public string Text { get; set; }
        // TODO: Add attachment
    }
}
