namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class DatabaseRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

        }
    }
}
