using FluentValidation;

namespace ShakSphere.Application.UseCases.AppUserProfile.Queries.QueryValidators
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }

}
