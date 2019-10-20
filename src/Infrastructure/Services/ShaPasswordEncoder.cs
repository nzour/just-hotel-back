using System;
using System.Security.Cryptography;
using System.Text;
using app.Application.Abstraction;
using kernel.Attribute;

namespace app.Infrastructure.Services
{
    [Transient(typeof(IPasswordEncoder))]
    public class ShaPasswordEncoder : IPasswordEncoder
    {
        private const string AlgorithmName = "SHA1";
        
        public string Encrypt(string passwordToEncode)
        {
            var bytes = Encoding.UTF8.GetBytes(passwordToEncode);

            var encoded = HashAlgorithm.Create(AlgorithmName)?.ComputeHash(bytes);

            if (null == encoded)
            {
                throw new Exception("Unable to create hash.");
            }

            return Convert.ToBase64String(encoded);
        }
    }
}