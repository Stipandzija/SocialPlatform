using ShakSphere.Application.Security;

namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class AuthRegistrar : IServiceRegistrar
    {
        private readonly JwtSettings _jwtSettings;
        public AuthRegistrar(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public void RegisterServices(WebApplicationBuilder builder)
        {
            if (string.IsNullOrEmpty(_jwtSettings.Issuer) || string.IsNullOrEmpty(_jwtSettings.SigningKey))
            {
                throw new Exception("JWT configuration is missing");
            }
            builder.Services.AddScoped<JwtTokenGenerator>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.ClaimsIssuer = _jwtSettings.Issuer;
                options.Audience = _jwtSettings.Audience;
                options.IncludeErrorDetails = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)
                    ),
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };
                options.Events = new JwtBearerEvents {
                    OnAuthenticationFailed = context => {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                          .RequireAuthenticatedUser());
            });
        }
    }
}
