using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading.Tasks;

namespace Cursus_Business.Common
{
    public class Encryption
    {
        private static readonly byte[] key;

        static Encryption()
        {
            try
            {
                key = Convert.FromBase64String("Qjh1cLs8zH1yWMqoqgo6uPCqs3S1vUvOlDO06nd9fqA=");
                ValidateKey();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize the encryption key.", ex);
            }
        }

        private static void ValidateKey()
        {
            // Ensure the key is 32 bytes (256 bits) for AES-256
            if (key.Length != 32)
            {
                throw new ArgumentException($"The encryption key must be 256 bits (32 bytes) long. Current length: {key.Length} bytes.");
            }
        }

        public static Task<string> HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            return Task.Run(() =>
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash returns byte array
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convert byte array to a string
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            });
        }

        public static string Encrypt(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            ValidateKey();

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.GenerateIV(); // Generate a new IV for each encryption

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length); // Prepend the IV to the ciphertext
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (cipherText == null)
                throw new ArgumentNullException(nameof(cipherText));

            ValidateKey();

            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public static string EncryptParameters(params string[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            string concatenatedParams = string.Join("|", parameters);
            return Encrypt(concatenatedParams);
        }

        public static string[] DecryptParameters(string encryptedText)
        {
            if (encryptedText == null)
                throw new ArgumentNullException(nameof(encryptedText));

            string decryptedText = Decrypt(encryptedText);
            return decryptedText.Split('|');
        }
    }
}
