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