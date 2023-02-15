using Contracts.Views.Attachment;
using Data.Entities;
using Mapster;

namespace Application.Interfaces.Mappers;

[Mapper]
public interface IAttachmentMapper
{
    AttachmentChatView MapToChatView(Attachment attachment);
    IEnumerable<AttachmentChatView> MapToChatView(IEnumerable<Attachment> attachments);
    AttachmentView MapToView(Attachment message);   
    IEnumerable<AttachmentView> MapToView(IEnumerable<Attachment> message);

    AttachmentDetailView MapToDetailView(Attachment message);
    IEnumerable<AttachmentDetailView> MapToDetailView(IEnumerable<Attachment> message);
}