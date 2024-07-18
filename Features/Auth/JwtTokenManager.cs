using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using clerk.server.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace clerk.server.Features.Auth;

public interface IJwtTokenManager
{
    string GenerateAccountAccessToken(Guid AccountId);
}

public class JwtTokenManager : IJwtTokenManager
{
    public string GenerateAccountAccessToken(Guid AccountId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, AccountId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: AppSettings.JwtOptions.Issuer,
            audience: AppSettings.JwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(1),
            signingCredentials: credentials
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}
