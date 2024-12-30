using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelrgatesDemo
{
    public delegate string HelloDelegate(string message);
    public class AnonymousFunc
    {

        public static string SayHello(string name) {
            return "Hello " + name + " Good Morning !..";
        }
        static void Main(string[] args)
        {
            HelloDelegate dd = SayHello;
            string s1= dd.Invoke("Manu");
            Console.WriteLine(s1);

            //Asynchronous 
            HelloDelegate d2 = delegate (string name)
            {
                return "Hello " + name + " Good Morning !..";
            };
            string s2=d2.Invoke("Manu");
            Console.WriteLine(s2);
        }
    }
}
