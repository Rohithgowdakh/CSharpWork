using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    public class GenericAtClassLevel<G>
    {
        public void Add(G a, G b)
        { 
            dynamic d1= a;
            dynamic d2= b;
            Console.WriteLine(d1+d2);
        }
       
    }
    public class Genn
    {
        public static void Main()
        {
            GenericAtClassLevel<int> demo = new GenericAtClassLevel<int>();
            demo.Add(1, 2);
        }
    }
}
