using FluentValidation;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands.CommandValidators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
