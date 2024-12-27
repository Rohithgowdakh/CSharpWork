using System;

namespace StructureDemo
{
    struct StructDemo
    {
        public int x;
        public StructDemo()
        {

            Console.WriteLine("Implicit constructor  in structure");
        }
        public StructDemo(int x)
        {
            this.x = x;
            Console.WriteLine("parameterized constructor  in structure");
        }

        public void Test()
        {
            Console.WriteLine("Structure Test Method");
        }
        static void Main()
        {
            StructDemo sd;
            sd.x = 5;
            sd.Test();
            StructDemo sd3 = new StructDemo();
            sd3.Test();
            StructDemo sd2 = new StructDemo(10);
            sd2.Test();
        }
    }
}
