using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Infrastructure.Data;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class DatabaseRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        }
    }
}
