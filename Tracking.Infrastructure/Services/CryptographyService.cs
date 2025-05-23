using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Tracking.Application.Common.Interface;

namespace Tracking.Infrastructure.Services
{
    public class Cryptography : ICryptography
    {
        private string Key = "V7MeCkZgH$@2&u;3";
        private string Iv = "hBF-?]_B22}4O';>";
        public string Encrypt(string Texto)
        {
            if (Texto == null || Texto.Length <= 0)
            {
                throw new ArgumentNullException("Texto a encriptar en blanco");
            }

            byte[] encrypted;

            using (Aes encrypt = Aes.Create())
            {
                encrypt.Key = Encoding.UTF8.GetBytes(this.Key);
                encrypt.IV = Encoding.UTF8.GetBytes(this.Iv);

                ICryptoTransform encryptor = encrypt.CreateEncryptor(encrypt.Key, encrypt.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(Texto);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Base64UrlEncoder.Encode(Convert.ToBase64String(encrypted));
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("Texto a desencriptar en blanco");
            }

            string plaintText = "";

            using (Aes encrypt = Aes.Create())
            {
                encrypt.Key = Encoding.UTF8.GetBytes(this.Key);
                encrypt.IV = Encoding.UTF8.GetBytes(this.Iv);

                ICryptoTransform decryptor = encrypt.CreateDecryptor(encrypt.Key, encrypt.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(Base64UrlEncoder.Decode(cipherText))))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintText;
        }
    }
}
