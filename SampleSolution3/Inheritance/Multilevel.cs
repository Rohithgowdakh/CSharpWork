using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
     class Multilevel
    {
        public Multilevel()
        {
            Console.WriteLine("Super Class Constructor ");
        }
    }
    class ss : Multilevel
    {
        public ss()
        {
            Console.WriteLine("Sub Class Constructor");
        }
    }
    class rr : ss
    {
        public  rr()
        {
            Console.WriteLine("Sub Class 2 Constructor");
        }
        static void Main(string[] args)
        {
            rr r = new rr();
        }
    }
}
