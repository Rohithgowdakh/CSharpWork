using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDemo1
{
    internal class Three
    {
        static void Main()
        {
            Program p=new Program();
            // p.Test1(); private only same class
           // p.Test3(); protected subclass only in another class
            p.Test2();
            p.Test4();
            p.Test5();
            
        }
    }
}
