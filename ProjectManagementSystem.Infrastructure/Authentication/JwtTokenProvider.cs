using Microsoft.Extensions.Configuration;
using ProjectManagementSystem.Application.Authentication.Commands.GenerateToken;
using ProjectManagementSystem.Application.Authentication.Dtos;
using ProjectManagementSystem.Application.Common.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace ProjectManagementSystem.Infrastructure.Authentication;

public class JwtTokenProvider(IConfiguration _configuration) : ITokenProvider
{
    public TokenDto GenerateToken(GenerateTokenCommand generateTokenCommand)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"]!;
        var key = jwtSettings["SecretKey"]!; // Fixed: Changed from "Secretkey" to "SecretKey"
        
        // Fixed: Use configured expiration time instead of DateTime.MaxValue
        var expirationMinutes = int.Parse(jwtSettings["TokenExpirationInMinutes"] ?? "10");
        var expires = DateTime.UtcNow.AddMinutes(expirationMinutes);
        
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, generateTokenCommand.Id!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, generateTokenCommand.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, generateTokenCommand.LastName!));
        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, generateTokenCommand.FirstName!));
        
        foreach (var role in generateTokenCommand.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        
        foreach (var Permission in generateTokenCommand.Permissions)
            claims.Add(new Claim("Permission", Permission));
        
        var descriptor = new SecurityTokenDescriptor();
        descriptor.Subject = new ClaimsIdentity(claims);
        descriptor.Expires = expires;
        descriptor.Issuer = issuer;
        descriptor.Audience = audience;
        descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);
        
        return new TokenDto
        {
            AccessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken = GenerateRefreshToken(), // Fixed: Generate unique refresh token
            Expires = expires
        };
    }
    
    /// <summary>
    /// Generates a cryptographically secure random refresh token
    /// </summary>
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
