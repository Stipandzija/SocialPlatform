using FluentValidation;
using MediatR;
using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
using ShakSphere.Application.Behaviors;
using ShakSphere.Application.UseCases.AppUserProfile.Queries;
using ShakSphere.Application.UseCases.AppUserProfile.Queries.QueryValidators;
using System.Reflection;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class MediatRRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersQuery>());

            //builder.Services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>(); primjer kako ubacit jedan po jedan
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                builder.Services.AddValidatorsFromAssembly(assembly);
            }

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(GenericValidationBehavior<,>));
        }
    }
}
