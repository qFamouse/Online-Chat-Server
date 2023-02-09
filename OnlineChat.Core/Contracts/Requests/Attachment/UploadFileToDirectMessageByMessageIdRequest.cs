using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contracts.Requests.Attachment
{
    public class UploadFileToDirectMessageByMessageIdRequest
    {
        public int MessageId { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
