using Contracts.Requests.Attachment;
using Contracts.Views.Attachment;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Commands.Attachments;

public class UploadFilesToDirectMessageByMessageIdCommand : IRequest<List<AttachmentChatView>>
{
    public int MessageId { get; set; }
    public IFormFileCollection Files { get; set; }

    public UploadFilesToDirectMessageByMessageIdCommand(UploadFilesToDirectMessageByMessageIdRequest request)
    {
        MessageId = request.MessageId;
        Files = request.Files;
    }

}