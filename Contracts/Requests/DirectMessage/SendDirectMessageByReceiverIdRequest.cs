using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.DirectMessage
{
    public class SendDirectMessageByReceiverIdRequest
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
