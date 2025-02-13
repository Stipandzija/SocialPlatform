namespace ShakSphere.API.Configuration.DependencyInjection.Abstractions
{
    public interface IWebApplicationRegister
    {
        void RegisterPipelineComponents(WebApplication app);
    }
}
