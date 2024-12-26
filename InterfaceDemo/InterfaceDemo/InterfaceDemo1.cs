using System;
namespace InterfaceDemo
{
    interface InterfaceTest1
    {
        void Test();
    }
    interface InterfaceTest2
    {
        void Test();
    }
    public abstract class TestClass
    {
        public abstract void Test1();
    }

    class InterfaceImplement : TestClass, InterfaceTest1, InterfaceTest2 
    {
        public void Test()
        {
            Console.WriteLine(" Multiple Interface  Test Method ");
        }
        void InterfaceTest1.Test()
        {
            Console.WriteLine("Interface 1 Test Method ");

        }

        void InterfaceTest2.Test()
        {
            Console.WriteLine("Interface 2 Test Method");
        }
        public override void Test1()
        {
            Console.WriteLine("Abstract Class Test Method");
        }
        static void Main(string[] args)
        {
            InterfaceImplement ii=new InterfaceImplement();
            ii.Test();
            ii.Test1 ();
            InterfaceTest1 i1 = ii;
            i1.Test();
            InterfaceTest2 i2 = ii;
            i2.Test();
        }

       
    }
}