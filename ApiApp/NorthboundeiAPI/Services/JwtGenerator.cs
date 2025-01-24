using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthboundeiAPI.Config;
using NorthboundeiAPI.IServices;
using NorthboundeiAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthboundeiAPI.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly AppSettings _appSettings;

        public JwtGenerator(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateToken(ApplicationUser user)
        {

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSettings.Jwt.Issuer,
                audience: _appSettings.Jwt.Audience,
                claims: claims,
                expires: user.ExpirationDate ?? DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateSecureKey()
        {
            byte[] keyBytes = new byte[32];
            RandomNumberGenerator.Fill(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }

    }

}
