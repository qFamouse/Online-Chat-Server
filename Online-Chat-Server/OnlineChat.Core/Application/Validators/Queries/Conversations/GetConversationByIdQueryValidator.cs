using Application.CQRS.Queries.Conversations;
using FluentValidation;

namespace Application.Validators.Queries.Conversations;

public sealed class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdQueryValidator()
    {
        // Conversation is exists
    }
}