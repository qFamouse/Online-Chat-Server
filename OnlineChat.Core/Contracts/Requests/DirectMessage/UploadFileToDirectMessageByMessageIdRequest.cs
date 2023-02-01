using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.DirectMessage
{
    public class UploadFileToDirectMessageByMessageIdRequest
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int MessageId { get; set; }
    }
}
