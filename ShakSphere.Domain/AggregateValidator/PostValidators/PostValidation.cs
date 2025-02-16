using FluentValidation;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Domain.AggregateValidator.PostValidators
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(x => x.TextContent)
                .NotNull().NotEmpty().WithMessage("Context shounot be null or ampty")
                .MaximumLength(1024).WithMessage("Post content max 1024 chars");
            RuleFor(x => x.AppUserId).NotEmpty().WithMessage("AppUserId is required");
        }
    }
}
