using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Command.CommandValidators
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().NotNull().WithMessage("PostId is required");
            RuleFor(x => x.text).NotEmpty().WithMessage("Text is required.");

        }
    }
}
