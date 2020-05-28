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
using Shop.Helpers;

namespace Shop.Handlers
{
    public class ProtectorHandler : IProtectorHandler
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IProtectionHelper _protectionHelper;
        private readonly IConfiguration _configuration;

        public ProtectorHandler(IDataProtectionProvider dataProtectionProvider, IProtectionHelper protectionHelper, IConfiguration configuration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _protectionHelper = protectionHelper;
            _configuration = configuration;
        }

        public string HashMd5(string name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(name)));

        public string Hashsha512(string name) =>
            BitConverter.ToString(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(name)));

        public string Protect(string name) =>
            _dataProtectionProvider
            .CreateProtector(_configuration["dataprotector"])
            .Protect(_protectionHelper.BuildToken(name));

        public string UnProtect(string name)
        {
            var token = _dataProtectionProvider.CreateProtector(_configuration["dataprotector"]).Unprotect(name);
            var handler = new JwtSecurityTokenHandler();

            return handler.CanReadToken(token) 
                ? handler.ReadJwtToken(token).Claims.First().Value
                : null;
        }

        public string HashMd5(Stream name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(name));
    }
}
