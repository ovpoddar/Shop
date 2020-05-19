using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Shop.Handlers.Interfaces;

namespace Shop.Handlers
{
    public class ProtectorHandler : IProtectorHandler
    {

        private readonly IDataProtector _dataProtector;
        private readonly IConfigurationRoot _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                                       .AddJsonFile("appsettings.Development.json")
                                                                                       .Build();
        public ProtectorHandler(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector(_configuration["dataprotector"]);
        }

        public string HashMd5(string name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(name)));

        public string Hashsha512(string name) =>
            BitConverter.ToString(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(name)));

        public string Protect(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return _dataProtector.Protect(tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration.GetSection("jwt")["expDate"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("jwt")["secret"])),
                    SecurityAlgorithms.HmacSha256Signature)
            })));
        }

        public string UnProtect(string name)
        {
            var token = _dataProtector.Unprotect(name);
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                return handler.ReadJwtToken(token).Claims.First().Value;
            }
            return null;
        }

        public string HashMd5(Stream name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(name));
    }
}
