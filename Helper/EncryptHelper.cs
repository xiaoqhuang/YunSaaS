using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public static class EncryptHelper
    {
        //计算MD5 Hash值
        public static string MD5Hash(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            bytes = md5.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            foreach (byte b in bytes)
            {
                result.Append(b.ToString("x2").ToUpper());
            }
            return result.ToString();
        }
    }
}
