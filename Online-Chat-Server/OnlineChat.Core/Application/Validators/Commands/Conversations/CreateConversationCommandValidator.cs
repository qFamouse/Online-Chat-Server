using Application.CQRS.Commands.Conversations;
using FluentValidation;

namespace Application.Validators.Commands.Conversations;

public sealed class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3);
    }
}