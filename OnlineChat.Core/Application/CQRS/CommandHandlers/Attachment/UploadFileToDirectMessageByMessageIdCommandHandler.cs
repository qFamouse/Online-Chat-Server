using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.Attachment;
using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Services.Abstractions;
using Configurations;
using Contracts.Requests.DirectMessage;
using Contracts.Views.Attachment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace Application.CQRS.CommandHandlers.Attachment
{
    internal class UploadFileToDirectMessageByMessageIdCommandHandler : IRequestHandler<UploadFileToDirectMessageByMessageIdCommand, List<AttachmentChatView>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IBlobService _blobService;
        private readonly AzureBlobConfiguration _azureBlobConfiguration;
        private readonly IAttachmentMapper _attachmentMapper;

        public UploadFileToDirectMessageByMessageIdCommandHandler
        (
            IDirectMessageRepository directMessageRepository, 
            IBlobService blobService, 
            IAttachmentRepository attachmentRepository, 
            IOptions<AzureBlobConfiguration> azureBlobOptions,
            IAttachmentMapper attachmentMapper
            )
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _azureBlobConfiguration = azureBlobOptions.Value ?? throw new ArgumentNullException(nameof(azureBlobOptions));
            _attachmentMapper = attachmentMapper ?? throw new ArgumentNullException(nameof(attachmentMapper));
        }

        public async Task<List<AttachmentChatView>> Handle(UploadFileToDirectMessageByMessageIdCommand request, CancellationToken cancellationToken)
        {
            var message = await _directMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
            string blobContainer = _azureBlobConfiguration.DirectMessagesContainer;

            var attachments = new List<Entities.Attachment>();

            foreach (IFormFile file in request.Files)
            {
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string filePath = $"{message.Id}/{message.ReceiverId}";

                string blobFileName = $"{filePath}/{uniqueFileName}";

                await using (var stream = file.OpenReadStream())
                {
                    var properties = await _blobService.UploadFileBlobAsync(blobContainer, stream, blobFileName, file.ContentType, cancellationToken);

                    var attachment = new Entities.Attachment()
                    {
                        OriginalName = file.FileName, // TODO: Check FileName and Name 
                        BlobName = uniqueFileName,
                        BlobPath = filePath,
                        ContentType = properties.ContentType,
                        ContentLength = properties.ContentLength,
                        DirectMessageId = message.Id
                    };

                    await _attachmentRepository.InsertAsync(attachment, cancellationToken);
                    await _attachmentRepository.Save(cancellationToken); // TODO: Save after all loading? - No because we can get exception
                    attachments.Add(attachment);
                }
            }

            var result = _attachmentMapper.MapToChatView(attachments).ToList();

            return result;
        }
    }
}
