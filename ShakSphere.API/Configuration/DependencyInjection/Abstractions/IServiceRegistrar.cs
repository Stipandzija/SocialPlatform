namespace ShakSphere.API.Configuration.DependencyInjection.Abstractions
{
    public interface IServiceRegistrar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
