using System;
namespace VariablesDemo
{
    internal class Program
    {
        int x;
        static int y=100;
        const int n = 10;
        readonly int r;
        public Program(int x,int z)
        {
            this.r = z;
            this.x = x;
        }
         static void Main(string[] args)
        {
            Console.WriteLine(y);
            Console.WriteLine(n);//we directly call constant variable w/o any instance
            Program p = new Program(10, 20);
            Program p1 = new Program(30, 40);
            Console.WriteLine(p.x+" "+p1.x);
            Console.WriteLine(p.r + " " + p1.r);
            //p.r = 10; not possible 
        }
    }
}