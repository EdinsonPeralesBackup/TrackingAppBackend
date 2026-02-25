using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Tracking.Application.Common.Interface;
using Tracking.Application.Common.Settings;

namespace Tracking.Infrastructure.Services
{
    public class Cryptography : ICryptography
    {
        private string _Key;
        private string _Iv;
        private readonly IConfiguration _configuration;

        public Cryptography(IConfiguration configuration)
        {
            this._configuration = configuration;
            var keyEncrypt = this._configuration.GetSection("KeyEncrypt").Get<KeyEncrypt>();
            this._Key = keyEncrypt.Key;
            this._Iv = keyEncrypt.Iv;
        }
        public string Encrypt(string Texto)
        {
            if (Texto == null || Texto.Length <= 0)
            {
                throw new ArgumentNullException("Texto a encriptar en blanco");
            }

            byte[] encrypted;

            using (Aes encrypt = Aes.Create())
            {
                encrypt.Key = Encoding.UTF8.GetBytes(this._Key);
                encrypt.IV = Encoding.UTF8.GetBytes(this._Iv);

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
                encrypt.Key = Encoding.UTF8.GetBytes(this._Key);
                encrypt.IV = Encoding.UTF8.GetBytes(this._Iv);

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
