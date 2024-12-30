using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelrgatesDemo
{
   
    public class SimplifiedDelegates
    {
        static void Main(string[] args)
        {
            //Func keyword is used when method return a value 
            Func<int,float,double,double> d1 = (x, y, z) =>   x + y + z; 
            double res = d1.Invoke(10, 11.11f, 45.234);
            Console.WriteLine(res);

            //Action keyword is used when method doesn't return a value  
            Action<int, float, double> d2 = (x, y, z) =>  Console.WriteLine(x + y + z); 
            d2.Invoke(10, 11.11f, 45.234);

            //Predicate keyword is used when method  return a bool value
            Predicate<string> d3 = (name) => {
                if (name.Length > 5) 
                    return true;
                return false;
            };
            bool res2 = d3.Invoke("Rohith");
            Console.WriteLine(res2);
        }

    }
}
