namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public static class ServiceRegistrar
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            new ControllersRegistrar().RegisterServices(builder);

            var registrars = scanningType.Assembly.GetTypes()
                .Where(t => typeof(IServiceRegistrar).IsAssignableFrom(t)
                    && !t.IsInterface
                    && !t.IsAbstract
                    && t != typeof(ControllersRegistrar))
                .Select(t => (IServiceRegistrar)ActivatorUtilities.CreateInstance(builder.Services.BuildServiceProvider(), t))
                .ToList();

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }
    }
}
