using System;
namespace AccessDemo2
{
    internal class Four : AccessDemo1.Program
    {
         static void Main(string[] args)
        {
            Four f=new Four();
            f.Test3();
            f.Test4();
            f.Test5();
           // f.Test1(); private only within same class 
           // f.Test2(); internal only within same project
        }
    }
}