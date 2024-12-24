using System;
namespace Inheritance
{
    class ClassA
    {
        public ClassA() {
            Console.WriteLine("Class A No-Args Constructor");
        }
        public ClassA(int a)
        {
            Console.WriteLine("Parent Class Constructor : "+a);
        }
        public void method()
        {
            Console.WriteLine("Class A No-Args Method");
        }

    }
    class ClassB : ClassA 
    {
        public ClassB() 
        {
            Console.WriteLine("Child Class Constructor");
        }
        public ClassB(int a):base(a)
        {
            
        }
        static void Main(string[] args)
        {
            ClassB b = new ClassB();
            ClassB b1 = new ClassB(10);
            ClassA aa;
            aa = b1;
            aa.method();

        }
    }
}