using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleTCP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Translator<object> binTranslator = new Translator<object>(new object());
            TCPHeader tcp = new TCPHeader();

            Console.WriteLine(tcp);
            Console.ReadLine();
        }
    }
}
