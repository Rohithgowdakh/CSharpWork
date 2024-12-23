using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Parameterized
    {
        public Parameterized(int i) {
            Console.WriteLine("Parameterized Constructor i :" + i);
        }
        static void Main(string[] args)
        {
            Parameterized p=new Parameterized(10);
            Parameterized p2 = new Parameterized(20);
            Parameterized p3 = p2;
        
        }

    }

}
