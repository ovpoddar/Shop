using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Shop.Handlers.Interfaces;
using Shop.Helpers.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Handlers
{
    public class ProtectorHandler : IProtectorHandler
    {
        public string HashMd5(Stream name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(name));
    }
}
