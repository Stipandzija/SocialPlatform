namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class MediatRRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersQuery>());

            //builder.Services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>(); primjer kako ubacit jedan po jedan
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                builder.Services.AddValidatorsFromAssembly(assembly);
            }

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(GenericValidationBehavior<,>));
        }
    }
}
