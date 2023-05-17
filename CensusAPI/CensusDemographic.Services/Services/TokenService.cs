using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CensusDemographic.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly SettingsOptions _settings;

        public TokenService(IOptions<SettingsOptions> settings)
        {
            _settings = settings.Value;
        }
        public Token GenerateToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                }),

                Expires = DateTime.UtcNow.AddHours(_settings.ExpireToken),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwtKey = new Token { JwtKey = tokenHandler.WriteToken(token) };

            return jwtKey;
        }
    }
}
