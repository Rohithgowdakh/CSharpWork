using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class Class2
    {
        public static void Main(string[] args)
        {
            int[] arr = { 23, 44, 12, 123, 44, 55, 213, 53, 1, 12, 3, 54, 12, 55, 11, 33, 42, 09 };
            var brr = from i in arr 
                      where i>50 
                      orderby i descending 
                      select i;
            foreach (var b in brr)
            {
                Console.Write(b+" ");
            }
        }
    }
}
