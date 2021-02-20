using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AirTrack.Core.Helpers
{
    public static class CryptoHelper
    {
        const string key = "YW5pbGFwaW9ncmVuaXlvcg==";


        public static string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            var passBytes = Encoding.UTF8.GetBytes(key);
            passBytes = SHA256.Create().ComputeHash(passBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passBytes);

            return Convert.ToBase64String(bytesEncrypted);

        }

        private static byte[] Encrypt(byte[] bytesToEncrypted , byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
              return null;

            var bytesToDecrypted = Convert.FromBase64String(encryptedText);
            var passBytes = Encoding.UTF8.GetBytes(key);

            passBytes = SHA256.Create().ComputeHash(passBytes);
            var bytesDecrypted = Decrypt(bytesToDecrypted, passBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Decrypt(byte[] bytesToEncrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToEncrypted, 0, bytesToEncrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }


    }
}
