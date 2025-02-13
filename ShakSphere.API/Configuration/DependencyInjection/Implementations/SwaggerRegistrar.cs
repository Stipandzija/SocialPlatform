using ShakSphere.API.Options;
using ShakSphere.API.Configuration.DependencyInjection.Abstractions;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class SwaggerRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
