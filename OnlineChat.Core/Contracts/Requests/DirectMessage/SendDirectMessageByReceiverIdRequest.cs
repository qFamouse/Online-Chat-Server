using System.Diagnostics.CodeAnalysis;

namespace Contracts.Requests.DirectMessage
{
    public class SendDirectMessageByReceiverIdRequest
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
