using ShakSphere.API.Configuration.Middleware.Abstractions;

namespace ShakSphere.API.Configuration.Middleware.Implementations
{
    public class ExceptionHandlingRegistrar : IMiddlewareRegistrar
    {
        public void RegisterMiddleware(WebApplication app)
        {
            app.UseExceptionHandler("/error");
        }
    }
}
