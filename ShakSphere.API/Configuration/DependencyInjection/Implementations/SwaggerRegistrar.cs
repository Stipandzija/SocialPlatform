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
