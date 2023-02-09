using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Requests.Attachment;
using Contracts.Views.Attachment;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Commands.Attachment
{
    public class UploadFileToDirectMessageByMessageIdCommand : IRequest<List<AttachmentChatView>>
    {
        public int MessageId { get; set; }
        public IFormFileCollection Files { get; set; }

        public UploadFileToDirectMessageByMessageIdCommand(UploadFileToDirectMessageByMessageIdRequest request)
        {
            MessageId = request.MessageId;
            Files = request.Files;
        }

    }
}
