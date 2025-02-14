using FluentValidation;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands.CommandValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand> // registriramo u ValidationBehavior prije Handlera
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
