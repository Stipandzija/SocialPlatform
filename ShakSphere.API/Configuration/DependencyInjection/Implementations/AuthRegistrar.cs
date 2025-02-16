namespace ShakSphere.API.Configuration.DependencyInjection.Implementations
{
    public class AuthRegistrar : IServiceRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            var issuer = configuration["JWT:Issuer"];
            var audience = configuration.GetSection("JWT:Audience").Get<string>();
            var signingKey = configuration["JWT:SigningKey"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(signingKey))
            {
                throw new Exception("JWT configuration is missing");
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.ClaimsIssuer = issuer;
                options.Audience = audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(signingKey)
                    ),
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    ValidTypes = new[] { "JWT" }
                };
            });
        }
    }
}
