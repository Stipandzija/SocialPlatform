using FluentValidation;

namespace ShakSphere.Application.UseCases.Auth.Commands.Validators
{
    public class RegisterUserIdentityCommandValidator : AbstractValidator<RegisterUserIdentityCommand>
    {
        public RegisterUserIdentityCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
