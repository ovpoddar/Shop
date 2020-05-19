using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Helpers
{
    public class ProtectionHelper : IProtectionHelper
    {
        private readonly IConfiguration _configuration;

        public ProtectionHelper(IConfiguration configuration) =>
            _configuration = configuration;
        
        public string BuildToken(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration.GetSection("jwt")["expDate"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("jwt")["secret"])),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
