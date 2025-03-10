using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.Models;



namespace ShakSphere.Application.Behaviors
{
    public class GenericValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public GenericValidationBehavior(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is INoValidationRequired)
            {
                return await next();
            }


            if (_validator == null)
            {
                return await next();
            }
            //validacija
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid)
            {
                return await next();
            }

            // ukoliko smo greskom response zamjenili s necnim drugom sto nije ResponseStatus
            if (typeof(TResponse).GetGenericTypeDefinition() != typeof(ResponseStatus<>))
            {
                throw new InvalidOperationException("Response should be response typeof ResponseStatus<T>");
            }

            var responseType = typeof(TResponse).GetGenericArguments()[0]; // Dohvati T iz ResponseStatus<T>
            //generiraj response kao novu istancu ResponseStatus<T> npr ResponseStatus<ApplicationUser>
            var response = Activator.CreateInstance(typeof(ResponseStatus<>).MakeGenericType(responseType))!;

            var responseStatus = (ResponseStatus<object>)response;//castamo na generiki tip radi pristupa svostvima tipa
            responseStatus.Errors = new List<ProblemDetails>();

            for (int i = 0; i < validation.Errors.Count; i++)
            {
                var error = validation.Errors[i];
                responseStatus.Errors.Add(new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = error.ErrorMessage,
                    Status = 400
                });
            }
            responseStatus.Success = false;

            return (TResponse)response;
        }
    }
}
