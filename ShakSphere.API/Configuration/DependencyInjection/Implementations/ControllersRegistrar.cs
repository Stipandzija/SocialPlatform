using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using ShakSphere.API.Configuration.DependencyInjection.Abstractions;
using ShakSphere.Application.Models;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class ControllersRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

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
