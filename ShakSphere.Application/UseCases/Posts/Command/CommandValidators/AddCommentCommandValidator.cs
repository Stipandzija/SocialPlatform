using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Command.CommandValidators
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.PostId).NotEmpty().WithMessage("PostId is required");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is empty");
        }
    }
}
