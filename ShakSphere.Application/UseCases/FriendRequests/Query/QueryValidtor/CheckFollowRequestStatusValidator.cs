using FluentValidation;

namespace ShakSphere.Application.UseCases.FriendRequests.Query.QueryValidtor
{
    public class CheckFollowRequestStatusValidator : AbstractValidator<CheckFollowRequestStatusQuery>
    {
        public CheckFollowRequestStatusValidator()
        {
            RuleFor(x => x.SenderId).NotEmpty().WithMessage("SenderId is required");
            RuleFor(x => x.ReceiverId).NotEmpty().WithMessage("ReceiverId is required");
            RuleFor(x => x).Must(x => x.SenderId != x.ReceiverId).WithMessage("SenderId and ReceiverId cannot be the same");
        }
    }
}