using System;
namespace MethodOverriding
{
    public class Super
    {
        public virtual void Test()
        {
            Console.WriteLine("Parent Class Test Method ");
        }
        public void Test2()
        {
            Console.WriteLine("Parent Class Test 2 Method");//Method Hiding/Shadowing
        }

    }
    public class Sub : Super
    {
        public override void Test()
        {
            base.Test();
            Console.WriteLine("Child Class Test Method");
        }
        public new void Test2()// here new is optional 
        {
            base.Test2();
            Console.WriteLine("Child Class Test 2 Method");//Method Hiding/Shadowing
        }
        static void Main(string[] args)
        {
            Sub ss = new Sub();//in override by using super class reference we can call subclass method after override
            ss.Test();
            ss.Test2();
            Super s2 = new Sub();
            s2.Test2(); //in hiding by using super class reference we cant call subclass method after override
            s2.Test();
        }
    }
}
