using FluentValidation;

namespace ShakSphere.Application.UseCases.FriendRequests.Command.CommandValidator
{
    public class AcceptFriendRequestCommandValidator : AbstractValidator<AcceptFriendRequestCommand>
    {
        public AcceptFriendRequestCommandValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("RequestId cannot be empty.");
        }
    }
}
