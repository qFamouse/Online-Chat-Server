using Application.Interfaces.Repositories;
using FluentValidation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.DirectMessage;
using Microsoft.AspNetCore.Identity;
using Resources;

namespace Application.Validators.Commands.DirectMessage
{
    public sealed class SendDirectMessageByReceiverIdCommandValidator : AbstractValidator<SendDirectMessageByReceiverIdCommand>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;
        private readonly UserManager<Entities.User> _userManager;

        public SendDirectMessageByReceiverIdCommandValidator(IDirectMessageRepository directMessageRepository, IIdentityService identityService, UserManager<Entities.User> userManager)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));


            RuleFor(x => x.ReceiverId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(UserMustBeExists).WithMessage(Messages.NotFound);

            RuleFor(x => x.TimeToLive)
                .GreaterThan(5)
                .LessThan(60);
        }

        private async Task<bool> UserMustBeExists(int userId, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(userId.ToString()) != null;
        }
    }
}
