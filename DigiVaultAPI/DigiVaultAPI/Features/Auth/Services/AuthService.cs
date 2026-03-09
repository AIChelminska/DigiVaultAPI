using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigiVaultAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace DigiVaultAPI.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string HashPassword(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        return hash;
    }

    public bool VerifyPassword(string password, string hash)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(password, hash);
        return isValid;
    }

    public string GenerateToken(User user)
    {
        var key = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var expiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"]!);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("IdUser", user.IdUser.ToString()),
            new Claim("Login", user.Login),
            new Claim("Role", user.Role.ToString()),
            new Claim("FirstName", user.FirstName)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
}
