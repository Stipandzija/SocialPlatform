using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Query.QueryValidators
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("PostId is required.")
                .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
        }

    }
}
