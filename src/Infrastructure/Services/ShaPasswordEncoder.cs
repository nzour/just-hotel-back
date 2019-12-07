using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public interface IPasswordEncoder
    {
        string Encrypt(string passwordToEncode);
    }

    public class ShaPasswordEncoder : IPasswordEncoder
    {
        private const string AlgorithmName = "SHA1";

        public string Encrypt(string passwordToEncode)
        {
            var bytes = Encoding.UTF8.GetBytes(passwordToEncode);

            var encoded = HashAlgorithm.Create(AlgorithmName)?.ComputeHash(bytes)
                          ?? throw new Exception("Unable to create hash.");

            return Convert.ToBase64String(encoded);
        }
    }
}