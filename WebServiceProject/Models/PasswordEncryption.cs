using System.Security.Cryptography;
using System.Text;

namespace WebServiceProject.Models
{
    public class PasswordEncryption
    {
        private static readonly string EncryptionKey = "kha9lil."; // Changez pour une clé robuste

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(16).Substring(0, 16));
                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
    }
}
