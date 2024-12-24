using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class ConstructorOverload
    {
        public ConstructorOverload(int a) {
            
            Console.WriteLine(" The First Constructor :"+a);
        }
        public ConstructorOverload(int a,int b):this(a)
        {
            Console.WriteLine(" The Second Constructor :" + a + " " + b);
        }
        static void Main(string[] args)
        {
            ConstructorOverload cc = new ConstructorOverload(10, 30);
        }
    }
}
