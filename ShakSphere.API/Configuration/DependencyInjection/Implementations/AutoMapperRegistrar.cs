using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
using ShakSphere.API.Options;
using ShakSphere.Application.UseCases.AppUserProfile.Queries;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class AutoMapperRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program),typeof(GetAllUsersQuery));
        }
    }
}