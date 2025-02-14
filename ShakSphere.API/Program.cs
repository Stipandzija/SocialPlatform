
using ShakSphere.API.Configuration.DependencyInjection.Implementations;
using ShakSphere.API.Configuration.Middleware.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));

var app = builder.Build();


app.RegisterMiddleware(typeof(Program));


app.Run();
