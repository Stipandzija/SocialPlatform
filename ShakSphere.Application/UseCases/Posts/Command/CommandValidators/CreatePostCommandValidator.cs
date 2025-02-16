using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Command.CommandValidators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.TextContent)
                .NotEmpty().WithMessage("TextContent is required");
        }
    }
}
