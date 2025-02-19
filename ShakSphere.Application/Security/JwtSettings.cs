namespace ShakSphere.Application.Security
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

}
