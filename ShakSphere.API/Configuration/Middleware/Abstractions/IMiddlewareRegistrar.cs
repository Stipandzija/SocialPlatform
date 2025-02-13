namespace ShakSphere.API.Configuration.Middleware.Abstractions
{
    public interface IMiddlewareRegistrar
    {
        void RegisterMiddleware(WebApplication app);
    }
}
