using Application.CQRS.Commands.Conversation;
using FluentValidation;

namespace Application.Validators.Commands.Conversation;

public sealed class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3);
    }
}