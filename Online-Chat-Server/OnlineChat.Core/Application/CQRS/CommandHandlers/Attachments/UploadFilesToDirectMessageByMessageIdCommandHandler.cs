using Application.CQRS.Commands.Attachments;
using Application.Mappers.Abstractions;
using Configurations;
using Contracts.Views.Attachment;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repositories.Abstractions;
using Services.Abstractions;

namespace Application.CQRS.CommandHandlers.Attachments;

internal class UploadFilesToDirectMessageByMessageIdCommandHandler : IRequestHandler<UploadFilesToDirectMessageByMessageIdCommand, List<AttachmentChatView>>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IBlobService _blobService;
    private readonly AzureBlobConfiguration _azureBlobConfiguration;
    private readonly IAttachmentMapper _attachmentMapper;

    public UploadFilesToDirectMessageByMessageIdCommandHandler
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

    public async Task<List<AttachmentChatView>> Handle(UploadFilesToDirectMessageByMessageIdCommand request, CancellationToken cancellationToken)
    {
        var message = await _directMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        string blobContainer = _azureBlobConfiguration.DirectMessagesContainer;

        var attachments = new List<Attachment>();

        foreach (IFormFile file in request.Files)
        {
            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = $"{message.SenderId}/{message.ReceiverId}";

            string blobFileName = $"{filePath}/{uniqueFileName}";

            await using (var stream = file.OpenReadStream())
            {
                var properties = await _blobService.UploadFileBlobAsync(blobContainer, stream, blobFileName, file.ContentType, cancellationToken);

                var attachment = new Attachment()
                {
                    OriginalName = file.FileName,
                    BlobName = uniqueFileName,
                    BlobPath = filePath,
                    ContentType = properties.ContentType,
                    ContentLength = properties.ContentLength,
                    DirectMessageId = message.Id
                };

                await _attachmentRepository.InsertAsync(attachment, cancellationToken);
                await _attachmentRepository.Save(cancellationToken);
                attachments.Add(attachment);
            }
        }

        var result = _attachmentMapper.MapToChatView(attachments).ToList();

        return result;
    }
}