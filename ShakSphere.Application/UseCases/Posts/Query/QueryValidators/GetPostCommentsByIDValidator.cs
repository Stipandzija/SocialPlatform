using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Query.QueryValidators
{
    public class GetPostCommentsByIDValidator : AbstractValidator<GetPostCommentsByIDQuery>
    {
        public GetPostCommentsByIDValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().NotNull().WithMessage("PostId is required");
        }
    }
}
