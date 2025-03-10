using FluentValidation;
namespace ShakSphere.Application.UseCases.AppUserProfile.Queries.QueryValidators
{
    public class GetUsersByIdsQueryValidator : AbstractValidator<GetUsersByIdsQuery>
    {
        public GetUsersByIdsQueryValidator()
        {
            RuleFor(x => x.UserIds).NotNull().WithMessage("User ID list is empty");
            RuleForEach(x => x.UserIds).NotEmpty().WithMessage("User ID cannot be empty");
        }
    }
}
