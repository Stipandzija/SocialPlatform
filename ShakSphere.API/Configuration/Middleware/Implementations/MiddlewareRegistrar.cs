using ShakSphere.API.Configuration.DependencyInjection.Abstractions;

namespace ShakSphere.API.Configuration.Middleware.Implementations
{
    public static class MiddlewareRegistrar
    {
        public static void RegisterMiddleware(this WebApplication app, Type scanningType)
        {
            var registrars = scanningType.Assembly.GetTypes()
                .Where(t => typeof(IWebApplicationRegister).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IWebApplicationRegister>();

            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineComponents(app);
            }
        }

    }
}
