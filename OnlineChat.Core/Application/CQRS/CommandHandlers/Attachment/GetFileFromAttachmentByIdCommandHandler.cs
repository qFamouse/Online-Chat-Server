using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.Attachment;
using Application.Interfaces.Repositories;
using Application.Services.Abstractions;
using Azure.Storage.Blobs.Models;
using Configurations;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.CQRS.CommandHandlers.Attachment
{
    internal class GetFileFromAttachmentByIdCommandHandler : IRequestHandler<GetFileFromAttachmentByIdCommand, BlobDownloadInfo>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IBlobService _blobService;
        private readonly AzureBlobConfiguration _blobConfiguration;

        public GetFileFromAttachmentByIdCommandHandler
        (
            IAttachmentRepository attachmentRepository,
            IBlobService blobService,
            IOptions<AzureBlobConfiguration> azureBlobOptions
        )
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
            _blobConfiguration = azureBlobOptions.Value ?? throw new ArgumentNullException(nameof(azureBlobOptions));
        }

        public async Task<BlobDownloadInfo> Handle(GetFileFromAttachmentByIdCommand request, CancellationToken cancellationToken)
        {
            var attachment = await _attachmentRepository.GetByIdAsync(request.Id, cancellationToken);

            string containerName = _blobConfiguration.DirectMessagesContainer;
            string fileName = $"{attachment.Path}/{attachment.TimestampName}";

            var downloadInfo = await _blobService.GetBlobAsync(containerName, fileName);

            return downloadInfo;
        }
    }
}
