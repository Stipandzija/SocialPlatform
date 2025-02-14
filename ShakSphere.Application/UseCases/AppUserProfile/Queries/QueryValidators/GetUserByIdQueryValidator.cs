using FluentValidation;
using ShakSphere.Application.Behaviors;


namespace ShakSphere.Application.UseCases.AppUserProfile.Queries.QueryValidators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().Must(BeAValidGuid).WithMessage("Not valid GUID format");
        }

        private bool BeAValidGuid(Guid userId)
        {
            return Guid.TryParse(userId.ToString(), out _);
        }
    }
}
