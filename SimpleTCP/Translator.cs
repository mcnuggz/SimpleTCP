using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleTCP
{
    public class Translator<T>
    {
        public object awesomeObject;
        public byte[] byteArray;
        public Translator(T value)
        {
            if (value == null)
            {
                Console.WriteLine("Null");
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memStream, value);
                byteArray = memStream.ToArray();
                Console.WriteLine(Encoding.Default.GetString(byteArray));
            }
        }

        private object byteArrayToObject(byte[] arrbytes)
        {
            MemoryStream memstream = new MemoryStream();
            BinaryFormatter binform = new BinaryFormatter();
            memstream.Write(arrbytes, 0, arrbytes.Length);
            memstream.Seek(0, SeekOrigin.Begin);
            object obj = binform.Deserialize(memstream);
            return obj;
        }
    }
}
