using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fanex.Encrypt.Business
{
    public static class AesEncrypt2
    {
        public static string EncryptDataWithAes(string plainText, string keyBase32, string vectorBase16)
        {
            if (string.IsNullOrEmpty(keyBase32) || keyBase32.Length != 32)
            {
                throw new Exception("The keyBase32 must be 32 characters");
            }

            if (string.IsNullOrEmpty(vectorBase16) || vectorBase16.Length != 16)
            {
                throw new Exception("The vectorBase32 must be 16 characters");
            }

            using Aes aesAlgorithm = Aes.Create();
            byte[] key = Encoding.UTF8.GetBytes(keyBase32);
            byte[] iv = Encoding.UTF8.GetBytes(vectorBase16);

            aesAlgorithm.Key = key;
            aesAlgorithm.IV = iv;

            var encryptor = aesAlgorithm.CreateEncryptor();

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            var encryptedData = ms.ToArray();

            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptDataWithAes(string cipherText, string keyBase32, string vectorBase16)
        {
            if (string.IsNullOrEmpty(keyBase32) || keyBase32.Length != 32)
            {
                throw new Exception("The keyBase32 must be 32 characters");
            }

            if (string.IsNullOrEmpty(vectorBase16) || vectorBase16.Length != 16)
            {
                throw new Exception("The vectorBase32 must be 16 characters");
            }

            using Aes aesAlgorithm = Aes.Create();
            byte[] key = Encoding.UTF8.GetBytes(keyBase32);
            byte[] iv = Encoding.UTF8.GetBytes(vectorBase16);

            aesAlgorithm.Key = key;
            aesAlgorithm.IV = iv;

            var decryptor = aesAlgorithm.CreateDecryptor();
            var cipher = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}