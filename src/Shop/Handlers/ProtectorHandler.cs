using Shop.Handlers.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Shop.Handlers
{
    public class ProtectorHandler : IProtectorHandler
    {
        public string HashMd5(Stream name) =>
            BitConverter.ToString(MD5.Create().ComputeHash(name));
    }
}
