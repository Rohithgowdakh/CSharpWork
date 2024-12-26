using System;
namespace AbstractClassDemo
{
    abstract class Calculator
    {
        public void Add(int x, int y)
        {
            Console.WriteLine("Addition :"+(x + y));
        }
        public void Sub(int x, int y)
        {
            Console.WriteLine("Subtraction :"+(x - y));
        }
        public abstract void mul(int x, int y);
        public abstract void div(int x, int y);
    }
    class CalculatorImpl : Calculator
    {
        public override void div(int x, int y)
        {
            Console.WriteLine("Division :"+(x / y));
        }

        public override void mul(int x, int y)
        {
            Console.WriteLine("Multiplication :"+(x*y));
        }
        static void Main(string[] args)
        {
            Calculator impl = new CalculatorImpl();

            impl.Add(10, 20);
            impl.Sub(122, 21);
            impl.mul(21, 43);
            impl.div(100, 4);
            CalculatorImpl cc= new CalculatorImpl();
            cc.mul(100, 4);
            cc.div(960, 4);
        }
    }
}