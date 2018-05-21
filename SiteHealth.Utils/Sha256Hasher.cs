using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BotMagic.Utils
{
    public class Sha256Hasher : ISha256Hasher
    {
        private readonly HashAlgorithm hash = new SHA256Managed();

        public string HashString(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] result = hash.ComputeHash(bytes);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
