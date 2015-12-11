using System;

namespace SimpleTCP
{
    public class Program
    {
        public static void Main()
            {
                string input = "8000";
                var bytes = HexToBytes(input);
                string hex = Crc16.ComputeChecksum(bytes).ToString("x2");
                Console.WriteLine(hex); //c061            
            
            //Translator<object> binTranslator = new Translator<object>(new object());
            //TCPHeader tcp = new TCPHeader();

            //Console.WriteLine(tcp);
            //Console.ReadLine();
            }
        static byte[] HexToBytes(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
            }
            return result;
        }
    }
}
