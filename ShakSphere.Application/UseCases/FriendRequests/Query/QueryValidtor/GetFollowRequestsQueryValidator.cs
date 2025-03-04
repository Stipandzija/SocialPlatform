using FluentValidation;

namespace ShakSphere.Application.UseCases.FriendRequests.Query.QueryValidtor
{
    public class GetFollowRequestsQueryValidator : AbstractValidator<GetFollowRequestsQuery>
    {
        public GetFollowRequestsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}
