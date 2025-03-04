using FluentValidation;

namespace ShakSphere.Application.UseCases.FriendRequests.Query.QueryValidtor
{
    public class GetUserFriendsQueryValidator : AbstractValidator<GetUserFriendsQuery>
    {
        public GetUserFriendsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        }
    }
}
