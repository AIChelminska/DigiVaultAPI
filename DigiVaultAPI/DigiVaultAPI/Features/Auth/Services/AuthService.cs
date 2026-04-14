using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigiVaultAPI.Data;
using DigiVaultAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace DigiVaultAPI.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly DigiVaultDbContext _context;

    public AuthService(IConfiguration configuration, DigiVaultDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);

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
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}