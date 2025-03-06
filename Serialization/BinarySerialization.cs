using System;
using System.IO;
using MessagePack;

namespace Serialization
{
    public class BinarySerialization : ISerializer
    {
        // Sérialisation binaire en utilisant MessagePack
        public void Serialize<T>(T data, string filePath)
        {
            try
            {
                var binaryData = MessagePackSerializer.Serialize(data); // Convertir en binaire
                File.WriteAllBytes(filePath, binaryData); // Écrire dans un fichier
                Console.WriteLine($"Données sérialisées avec succès dans {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sérialisation binaire : {ex.Message}");
            }
        }

        // Désérialisation binaire en utilisant MessagePack
        public T Deserialize<T>(string filePath)
        {
            try
            {
                var binaryData = File.ReadAllBytes(filePath); // Lire les données binaires
                return MessagePackSerializer.Deserialize<T>(binaryData); // Convertir en objet
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la désérialisation binaire : {ex.Message}");
                return default;
            }
        }
    }
}
