using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.AppUserProfile.Commands;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.Behaviors
{
    public class ValidationBehavior : IPipelineBehavior<UpdateUserCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IValidator<UpdateUserCommand> _validator;
        public ValidationBehavior(IValidator<UpdateUserCommand> validator)
        {
            _validator = validator;
        }
        public async Task<ResponseStatus<ApplicationUser>> Handle(UpdateUserCommand request, RequestHandlerDelegate<ResponseStatus<ApplicationUser>> next, CancellationToken cancellationToken)
        {
            //before handler
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid)
            {
                return await next();// sljedece pozovi odgovarajuci handler
            }

            //after handler
            return new ResponseStatus<ApplicationUser>
            {
                Success = false,
                Errors = validation.Errors.Select(error => new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = error.ErrorMessage,
                    Status = 400
                }).ToList()
            };
        }
    }
}
