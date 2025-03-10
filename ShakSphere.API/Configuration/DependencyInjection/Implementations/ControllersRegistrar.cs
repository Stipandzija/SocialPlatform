using Microsoft.AspNetCore.RateLimiting;
using ShakSphere.Application.Security;
using System.Threading.RateLimiting;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class ControllersRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddSingleton<IServiceRegistrar>(sp =>
            {
                var jwtSettings = sp.GetRequiredService<IOptions<JwtSettings>>();
                return new AuthRegistrar(jwtSettings);
            });

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                }));

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();

        }
    }
}
