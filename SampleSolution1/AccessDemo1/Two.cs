using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDemo1
{
    internal class Two : Program
    {
        static void Main(string[] args)
        {
            Two two = new Two();
           // two.Test1(); private members can be accessible only within same class
            two.Test2();
            two.Test3();
            two.Test4();
            two.Test5();
            
        }
    }
}
