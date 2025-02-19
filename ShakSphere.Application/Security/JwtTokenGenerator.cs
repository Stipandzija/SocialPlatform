using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShakSphere.Application.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenGenerator
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var claims = await GetClaims(user);
        var tokenDescriptor = GetSecurityTokenDescriptor(claims);
        var token = CreateToken(tokenDescriptor);
        return WriteToken(token);
    }
    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
    private SecurityTokenDescriptor GetSecurityTokenDescriptor(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            SigningCredentials = creds,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };
    }
    private SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }

    private string WriteToken(SecurityToken token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
