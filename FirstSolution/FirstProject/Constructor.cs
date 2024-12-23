using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    
    internal class Constructor
    {
        int i;
        bool b;
        public Constructor() {
            Console.WriteLine("the  value of I :"+i);
            Console.WriteLine("the  value of b :" + b);

            int val =int.Parse( Console.ReadLine());
            Console.WriteLine($"the user input i :{val}");
        }
        public static void Main()
        {
            Constructor c = new Constructor();
            Another n= new Another();
        }
    }
    class Another
    {
        public int i;
        public Another()
        {
            Console.WriteLine("Another Class i :"+i);
        }

    }
}
