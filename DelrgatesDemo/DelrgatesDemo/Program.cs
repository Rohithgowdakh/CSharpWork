using System;
namespace DelegatesDemo
{
    public delegate void AddDigit(int a, int b);
    public delegate string SayHi(string s);
    class Program
    {
        public void AddNums(int a, int b)
        {
            Console.WriteLine(a +  b);
        }
        public static string SayHello(string name)
        {
            return "Hello " + name;
        }
        public static void Main(string[] args)
        {
            Program p=new Program();
            AddDigit an = new AddDigit(p.AddNums);
            SayHi sh = new SayHi(SayHello);
            string s = sh("Manu");
            Console.WriteLine(s);
            an(100, 200);
            an.Invoke(100, 100);
            string ss = sh.Invoke("Sai");
            Console.WriteLine(ss);
        }
    }
}
