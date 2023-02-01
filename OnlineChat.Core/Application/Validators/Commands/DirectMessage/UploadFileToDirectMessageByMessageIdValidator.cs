﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.DirectMessage;
using Application.Interfaces.Repositories;
using FluentValidation;
using Resources;
using Services.Interfaces;

namespace Application.Validators.Commands.DirectMessage
{
    public class UploadFileToDirectMessageByMessageIdValidator : AbstractValidator<UploadFileToDirectMessageByMessageIdCommand>
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
                .MustAsync(_directMessageRepository.ExistsAsync).WithMessage(Messages.MessageNotFound)
                .MustAsync(MustBeMessageOwner).WithMessage(Messages.AccessDenied);
        }

        private async Task<bool> MustBeMessageOwner(int messageId, CancellationToken cancellationToken)
        {
            int currentUserId = _identityService.GetUserId();
            var message = await _directMessageRepository.GetByIdAsync(messageId, cancellationToken);
            return message.SenderId == currentUserId;
        }
    }
}
