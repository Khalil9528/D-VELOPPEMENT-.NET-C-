using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    internal class SerializerFactory
    {
        public static ISerializer CreateSerializer(string type)
        {
            return type.ToLower() switch
            {
                "binary" => new BinarySerialization(),
                "xml" => new XmlSerialization(),
                _ => throw new ArgumentException("Type de sérialisation non supporté")

            };
        }
    }
}
