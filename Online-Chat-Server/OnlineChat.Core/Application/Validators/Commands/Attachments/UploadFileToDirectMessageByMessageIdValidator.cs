using Application.CQRS.Commands.Attachments;
using Application.Interfaces.Repositories;
using FluentValidation;
using MassTransit.Monitoring.Performance;
using Resources;
using Resources.Messages;
using Services.Interfaces;

namespace Application.Validators.Commands.Attachments;

public class UploadFileToDirectMessageByMessageIdValidator : AbstractValidator<UploadFilesToDirectMessageByMessageIdCommand>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;

    public UploadFileToDirectMessageByMessageIdValidator(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
    {
        _directMessageRepository = directMessageRepository;
        _identityService = identityService;

        RuleFor(x => x.MessageId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(_directMessageRepository.ExistsAsync).WithMessage(ChatMessages.MessageNotFound)
            .MustAsync(MustBeMessageOwner).WithMessage(BaseMessages.AccessDenied);
    }

    private async Task<bool> MustBeMessageOwner(int messageId, CancellationToken cancellationToken)
    {
        int currentUserId = _identityService.GetUserId();
        var message = await _directMessageRepository.GetByIdAsync(messageId, cancellationToken);
        return message.SenderId == currentUserId;
    }
}