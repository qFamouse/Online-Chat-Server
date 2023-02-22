using Azure.Storage.Blobs.Models;
using MediatR;

namespace Application.CQRS.Queries.Attachments;

public class GetFileFromAttachmentByIdQuery : IRequest<BlobDownloadInfo>
{
    public int Id { get; set; }

    public GetFileFromAttachmentByIdQuery(int id)
    {
        Id = id;
    }
}