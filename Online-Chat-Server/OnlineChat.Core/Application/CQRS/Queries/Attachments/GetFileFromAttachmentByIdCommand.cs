using Azure.Storage.Blobs.Models;
using MediatR;

namespace Application.CQRS.Queries.Attachments;

public class GetFileFromAttachmentByIdCommand : IRequest<BlobDownloadInfo>
{
    public int Id { get; set; }

    public GetFileFromAttachmentByIdCommand(int id)
    {
        Id = id;
    }
}