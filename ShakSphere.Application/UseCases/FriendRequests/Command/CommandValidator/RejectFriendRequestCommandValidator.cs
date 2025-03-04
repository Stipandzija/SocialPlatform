using FluentValidation;

namespace ShakSphere.Application.UseCases.FriendRequests.Command.CommandValidator
{
    public class RejectFriendRequestCommandValidator : AbstractValidator<RejectFriendRequestCommand>
    {
        public RejectFriendRequestCommandValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("RequestId cannot be empty");
        }
    }
}
