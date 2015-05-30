using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public static class EncryptHelper
    {
        private static string Des_Key = "U2FhUyNLZXk=";
        private static string Des_IV = "SVZPRiNERVM=";


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

        // Des加密方法
        public static string Encrypt(string input)
        {
            byte[] key = Convert.FromBase64String(Des_Key);
            byte[] iv = Convert.FromBase64String(Des_IV);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = key,
                IV = iv
            };
            ICryptoTransform transform = provider.CreateEncryptor(provider.Key, provider.IV);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        //DES解密方法
        public static string Decrypt(string input)
        {
            byte[] key = Convert.FromBase64String(Des_Key);
            byte[] iv = Convert.FromBase64String(Des_IV);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = key,
                IV = iv
            };
            ICryptoTransform transform = provider.CreateDecryptor(provider.Key, provider.IV);
            byte[] inputBytes = Convert.FromBase64String(input);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                cryptoStream.FlushFinalBlock();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}