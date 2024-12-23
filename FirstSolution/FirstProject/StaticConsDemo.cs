using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class StaticConsDemo
    {
        static StaticConsDemo()
        {
            Console.WriteLine("This is Implicitly auto Runnable Static Constructor AND it always execute first");
        }
        public static void Main()
        {
            Console.WriteLine("Main Method ");
        }
    }
}
