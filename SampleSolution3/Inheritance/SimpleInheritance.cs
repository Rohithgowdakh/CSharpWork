using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
     class SimpleInheritance
    {
        public virtual void Display()
        {
            Console.WriteLine("SuperClass Display Method");
        }
    }
    class SimpleInheritance2 : SimpleInheritance
    {
        public override void Display()
        {
            base.Display();
            Console.WriteLine("Subclass Display Method");
        }
        static void Main()
        {
            SimpleInheritance2 ss=new SimpleInheritance2();
            ss.Display();
        }
    }
}
