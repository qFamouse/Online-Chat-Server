using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using MediatR;

namespace Application.CQRS.Queries.Attachment;

public class GetFileFromAttachmentByIdCommand : IRequest<BlobDownloadInfo>
{
    public int Id { get; set; }

    public GetFileFromAttachmentByIdCommand(int id)
    {
        Id = id;
    }
}