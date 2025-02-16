using FluentValidation;

namespace ShakSphere.Application.UseCases.Posts.Command.CommandValidators
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("UserId is required");
        }
    }
}
