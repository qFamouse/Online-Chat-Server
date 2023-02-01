using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Requests.DirectMessage;
using Contracts.Views;
using MediatR;

namespace Application.CQRS.Commands.Attachment
{
    public class UploadFileToDirectMessageByMessageIdCommand : IRequest<AttachmentDetailView>
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int MessageId { get; set; }

        public UploadFileToDirectMessageByMessageIdCommand(UploadFileToDirectMessageByMessageIdRequest request)
        {
            FilePath = request.FilePath;
            FileName = request.FileName;
            MessageId = request.MessageId;
        }
    }
}
