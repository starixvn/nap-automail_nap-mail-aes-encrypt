using System;
using System.IO;
using System.Security.Cryptography;

namespace Fanex.Encrypt.Business
{
    public static class AesEncrypt
    {
        public static string EncryptDataWithAes(string plainText, out string keyBase64, out string vectorBase64)
        {
            using Aes aesAlgorithm = Aes.Create();
            keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
            vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

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

        public static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
        {
            using Aes aesAlgorithm = Aes.Create();
            aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
            aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

            var decryptor = aesAlgorithm.CreateDecryptor();
            var cipher = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}