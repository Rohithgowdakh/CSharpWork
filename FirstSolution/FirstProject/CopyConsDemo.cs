using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class CopyConsDemo
    {
        int n;
        public CopyConsDemo(int i) {
            n= i;
            Console.WriteLine("The Value of i :"+i);
        }
        public CopyConsDemo(CopyConsDemo obj) {
            Console.WriteLine("Copy Constructor Value :" + obj.n);
        }
        static void Main(string[] args)
        {
            CopyConsDemo c = new CopyConsDemo(10);
            CopyConsDemo copy = new CopyConsDemo(c);
        }
    }

}
