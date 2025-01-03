using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    public class ArrayListDemo
    {
        public void ArrayListWithoutGeneric()
        {
            ArrayList a1 = new ArrayList();
            a1.Add(1);
            a1.Add("Manu");
            a1.Add(10.23);
            a1.Add('C');
            a1.Add(true);
            Console.WriteLine(a1[1]);
            foreach (object demo in a1) Console.WriteLine(demo);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
        }
        public void RemoveElement()
        {
            List<string> remove = new List<string>();
            remove.Add("Mango");
            remove.Add("Apple");
            remove.Add("Gauva");
            remove.Add("Grapes");
            foreach (object demo in remove) Console.WriteLine(demo);
            Console.WriteLine();
            remove.Remove("Apple");
            remove.Remove(remove[2]);
            foreach (object demo in remove) Console.WriteLine(demo);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
            remove.ForEach(n => Console.WriteLine(n));

        }
    }
}
