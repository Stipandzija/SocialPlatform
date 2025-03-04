﻿using FluentValidation;


namespace ShakSphere.Application.UseCases.FriendRequests.Command.CommandValidator
{
    public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
    {
        public SendFriendRequestCommandValidator()
        {
            RuleFor(x => x.SenderId).NotEmpty().WithMessage("SenderId cannot be empty.");
            RuleFor(x => x.ReceiverId).NotEmpty().WithMessage("ReceiverId cannot be empty").NotEqual(x => x.SenderId).WithMessage("You cannot send a friend request to yourself");
        }
    }
}
