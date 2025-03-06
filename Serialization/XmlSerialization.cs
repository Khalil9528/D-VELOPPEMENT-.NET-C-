using System.IO;              
using System.Xml.Serialization;

namespace Serialization
{
    public class XmlSerialization : ISerializer
    {
        public void Serialize<T>(T data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Fichier introuvable.");

            using (StreamReader reader = new StreamReader(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }

}
