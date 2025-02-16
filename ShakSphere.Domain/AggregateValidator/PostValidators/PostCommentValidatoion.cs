using FluentValidation;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Domain.AggregateValidator.PostValidators
{
    public class PostCommentValidatoion : AbstractValidator<PostComment>
    {
        public PostCommentValidatoion()
        {
            RuleFor(X => X.DateOfCreation < X.LastModified);
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Text should not be null or empty")
                .MaximumLength(256).WithMessage("Max lenght 256 char");

        }
    }
}
