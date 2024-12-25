using System;
namespace MethodOverloading
{
     class Program
    {
        public static void Test()
        {
            Console.WriteLine("1st Method");
        }
        public static void Test(int i)
        {
            Console.WriteLine("2nd Method");
        }
        public static void Test(String s)
        {
            Console.WriteLine("3rd Method");
        }
        public static void Test(int i,String s)
        {
            Console.WriteLine("4th Method");
        }
        public static void Test(String s,int i)
        {
            Console.WriteLine("5th Method");
        }
        static void Main(string[] args)
        {
            Test();
            Test(10);
            Test("Rohith");
            Test(10, "Rohi");
            Test("Rohi", 100);
        }
    }
}