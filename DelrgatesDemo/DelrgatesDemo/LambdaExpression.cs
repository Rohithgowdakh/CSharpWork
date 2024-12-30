using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelrgatesDemo
{
    public delegate string Hello(string text);
    public class LambdaExpression
    {
        public static void Main(string[] args)
        {
            Hello h1 = (name) =>
            {
                return "Hello " + name + " Good Morning";
            };
            Console.WriteLine(h1.Invoke("Manu"));
        }
    }
}
