namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class PipelineRegistrar : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToString());
                }
            });

            app.UseRateLimiter();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
