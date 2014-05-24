using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ion.Tools
{
    public class DeepClone
    {
        public static T Object<T>(T objectToClone)
        {
            if (objectToClone == null)
                return default(T);

            if (!typeof(T).IsSerializable)
                throw new ArgumentException("The type must be serializable. Add [Serializable] to your class", objectToClone.GetType().ToString());

            Stream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            using (stream)
            {
                formatter.Serialize(stream, objectToClone);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
