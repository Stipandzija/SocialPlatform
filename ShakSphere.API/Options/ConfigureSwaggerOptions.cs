namespace ShakSphere.API.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description) 
        {
            var info = new OpenApiInfo
            {
                Title = $"ShakSphere API {description.ApiVersion}",
                Version = description.ApiVersion.ToString()
            };
            if (description.IsDeprecated) 
            {
                info.Description = "This Api version is deprecated";
            }
            return info;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
            var scheme = securityScheme();
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
            //zahtjev za sve rute
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { scheme, new string[] {} }
            });
        }
        private OpenApiSecurityScheme securityScheme()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter JWT token",
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };
        }
    }
}
