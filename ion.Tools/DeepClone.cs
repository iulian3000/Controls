namespace Ion.Tools
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Creates a deep clone of an object
    /// </summary>
    public class DeepClone
    {
        /// <summary>
        /// Clones object
        /// </summary>
        /// <typeparam name="T"> typeof the object </typeparam>
        /// <param name="objectToClone"> The object to cloned </param>
        /// <returns> Return result as T </returns>
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