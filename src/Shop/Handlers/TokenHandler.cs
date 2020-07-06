using DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public string GenerateToken(Employer employer) =>
            new JwtSecurityTokenHandler()
            .WriteToken(new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, $"{employer.FirstName} {employer.LastName}"),
                    new Claim(JwtRegisteredClaimNames.Email, employer.Email),
                    new Claim(JwtRegisteredClaimNames.Gender, employer.Gender),
                    new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddDays(1).ToShortDateString()),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, employer.UserName),
                    new Claim(JwtRegisteredClaimNames.Iat, employer.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                },
                audience: _configuration.GetSection("jwt")["Audiences"],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration.GetSection("Jwt")["secret"])), SecurityAlgorithms.HmacSha512),
                expires: DateTime.Now.AddDays(1)
            ));
    }
}
