using Application.CQRS.Commands.Attachments;
using FluentValidation;
using Repositories.Abstractions;
using Resources.Messages;
using Services.Abstractions;

namespace Application.Validators.Commands.Attachments;

public class UploadFileToDirectMessageByMessageIdValidator : AbstractValidator<UploadFilesToDirectMessageByMessageIdCommand>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;

    public UploadFileToDirectMessageByMessageIdValidator(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
    {
        _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        BuildValidation();
    }

    private void BuildValidation()
    {
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