var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));

var app = builder.Build();


app.RegisterMiddleware(typeof(Program));


app.Run();
