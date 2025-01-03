using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    public class Generics1
    {
        public void Add<G>(G a,G b)
        {
            dynamic d1 = a;
            dynamic d2 = b;
            Console.WriteLine(d1+d2);
        }
        public void Compare<G>(G a, G b)
        {
           Console.WriteLine(a.Equals(b));
        }
        public static void Main()
        {
            Generics1 g1=new Generics1();
            g1.Compare<int>(10, 10);
            g1.Add<int>(20, 20);
        }
    }
}
