using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Runner.Services
{
    public class CryptoService
    {
        string publicKey = "#SASD!@dsd34D5sml9)lf(-khsdjfsdkfsdfkjsdfik";
        string privateKey = "q2#r44C&m(%mytp8&";

        public static string Encrypt(string toEncrypt, string securityKey)
        {
            var key = securityKey;
            var keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, string securityKey)
        {
            var key = securityKey;
            var keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var toEncryptArray = Convert.FromBase64String(cipherString);
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
