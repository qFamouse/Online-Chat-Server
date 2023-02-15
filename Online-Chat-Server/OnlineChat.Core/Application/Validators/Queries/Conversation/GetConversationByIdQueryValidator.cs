using Application.CQRS.Queries.Conversation;
using FluentValidation;

namespace Application.Validators.Queries.Conversation;

public sealed class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdQueryValidator()
    {
        // Conversation is exists
    }
}