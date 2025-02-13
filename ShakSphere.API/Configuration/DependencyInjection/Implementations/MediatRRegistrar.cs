using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
using ShakSphere.Application.UseCases.AppUserProfile.Queries;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class MediatRRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersQuery>());
        }
    }
}
