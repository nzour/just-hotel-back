using System;
using System.Text;

namespace app.Common
{
    public static class EncodeHandler
    {
        public static string Base64Encode(string toEncode)
        {
            var bytes = Encoding.UTF8.GetBytes(toEncode);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decode(string toDecode)
        {
            var bytes = Convert.FromBase64String(toDecode);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string EncodePassword(string password)
        {
            return password;
        }
    }
}