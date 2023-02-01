﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.Attachment;
using Application.Entities;
using Application.Interfaces.Repositories;
using Application.Services.Abstractions;
using Configurations;
using Contracts.Requests.DirectMessage;
using Contracts.Views;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.CQRS.CommandHandlers.Attachment
{
    internal class UploadFileToDirectMessageByMessageIdCommandHandler : IRequestHandler<UploadFileToDirectMessageByMessageIdCommand, AttachmentDetailView>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IBlobService _blobService;
        private readonly AzureBlobConfiguration _azureBlobConfiguration;

        public UploadFileToDirectMessageByMessageIdCommandHandler(IDirectMessageRepository directMessageRepository, IBlobService blobService, IAttachmentRepository attachmentRepository, IOptions<AzureBlobConfiguration> azureBlobOptions)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _azureBlobConfiguration = azureBlobOptions.Value ?? throw new ArgumentNullException(nameof(azureBlobOptions));
        }

        public async Task<AttachmentDetailView> Handle(UploadFileToDirectMessageByMessageIdCommand request, CancellationToken cancellationToken)
        {
            var message = await _directMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);

            string storageFileName = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            string storageFilePath = $"{message.Id}/{message.ReceiverId}";

            await _blobService.UploadFileBlobAsync(_azureBlobConfiguration.DirectMessagesContainer, request.FilePath, $"{storageFilePath}/{storageFileName}");

            var attachment = new Entities.Attachment()
            {
                OriginalName = request.FileName,
                TimestampName = storageFileName,
                Path = storageFilePath,
                DirectMessageId = message.Id
            };

            await _attachmentRepository.InsertAsync(attachment, cancellationToken);
            await _attachmentRepository.Save(cancellationToken);

            return new AttachmentDetailView()
            {
                Id = attachment.Id,
                OriginalName = attachment.OriginalName,
                TimestampName = attachment.TimestampName,
                Path = attachment.Path,
                DirectMessageId = attachment.DirectMessageId,
                DirectMessage = new DirectMessageView()
                {
                    Id = message.Id,
                    Message = message.Message,
                    ReceiverId = message.ReceiverId,
                    SenderId = message.SenderId,
                    Time = message.UpdatedAt
                },
                CreatedAt = attachment.CreatedAt,
                UpdatedAt = attachment.UpdatedAt
            };
        }
    }
}