namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public static class ServiceRegistrar
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IServiceRegistrar>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IServiceRegistrar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
