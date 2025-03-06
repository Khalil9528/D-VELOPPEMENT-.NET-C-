using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Security.Principal;

public class FileEncryption
{
    /// <summary>
    /// Crypte un fichier donné avec une clé et un IV.
    /// </summary>
    public static void EncryptFile(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
    {
        using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputFileStream.CopyTo(cryptoStream);
            }
        }
    }

    /// <summary>
    /// Décrypte un fichier donné avec une clé et un IV.
    /// </summary>
    public static void DecryptFile(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
    {
        using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(outputFileStream);
            }
        }
    }

    /// <summary>
    /// Génère une clé à partir d'une chaîne utilisateur (ou SID Windows).
    /// </summary>
    public static byte[] GenerateKey(string userKey, int keySize = 32)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(userKey);
            byte[] hash = sha256.ComputeHash(keyBytes);
            Array.Resize(ref hash, keySize);
            return hash;
        }
    }

    /// <summary>
    /// Récupère le SID Windows de l'utilisateur courant.
    /// </summary>
    public static string GetWindowsSid()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        return identity.User?.Value ?? throw new InvalidOperationException("Unable to retrieve Windows SID.");
    }

    /// <summary>
    /// Crypte un fichier avec une clé utilisateur ou le SID Windows.
    /// </summary>
    public static void EncryptLibrary(string inputFilePath, string outputFilePath, string userProvidedKey = null)
    {
        string encryptionKey = userProvidedKey ?? GetWindowsSid(); // Clé utilisateur ou SID Windows
        byte[] key = GenerateKey(encryptionKey);

        using (Aes aes = Aes.Create())
        {
            byte[] iv = aes.IV;

            // Sauvegarde du IV
            File.WriteAllBytes(outputFilePath + ".iv", iv);

            // Crypter le fichier
            EncryptFile(inputFilePath, outputFilePath, key, iv);
        }
    }

    /// <summary>
    /// Décrypte un fichier avec une clé utilisateur ou le SID Windows.
    /// </summary>
    public static void DecryptLibrary(string inputFilePath, string outputFilePath, string userProvidedKey = null)
    {
        string encryptionKey = userProvidedKey ?? GetWindowsSid(); // Clé utilisateur ou SID Windows
        byte[] key = GenerateKey(encryptionKey);

        // Charger le IV sauvegardé
        byte[] iv = File.ReadAllBytes(inputFilePath + ".iv");

        // Décrypter le fichier
        DecryptFile(inputFilePath, outputFilePath, key, iv);
    }
}
