using Application.CQRS.Queries.Attachments;
using Application.Interfaces.Repositories;
using FluentValidation;
using Resources;
using Services.Interfaces;

namespace Application.Validators.Queries.Attachments;

public class GetFileFromAttachmentByIdValidator : AbstractValidator<GetFileFromAttachmentByIdCommand>
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IIdentityService _identityService;

    public GetFileFromAttachmentByIdValidator(IAttachmentRepository attachmentRepository, IIdentityService identityService)
    {
        _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(_attachmentRepository.ExistsAsync).WithMessage(Messages.NotFound)
            .MustAsync(MustBeMessageSenderOrMessageReceiverAsync).WithMessage(Messages.AccessDenied);
    }

    private async Task<bool> MustBeMessageSenderOrMessageReceiverAsync(int attachmentId, CancellationToken cancellationToken)
    {
        int currentUserId = _identityService.GetUserId();
        var detailAttachment = await _attachmentRepository.GetDetailByIdAsync(attachmentId, cancellationToken);

        return detailAttachment.DirectMessage.SenderId == currentUserId ||
               detailAttachment.DirectMessage.ReceiverId == currentUserId;
    }
}