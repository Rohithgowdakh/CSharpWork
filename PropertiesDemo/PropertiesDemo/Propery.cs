using System;
namespace PropertiesDemo
{
    public class Propery1
    {
        double _Radius = 12.32 ;
       
        public double Radius
        {
            get { return _Radius; }
            set { _Radius = value; }
            //set  {
            //if(value>_Radius)
            // _Radius = value;
            //      }
        }
    }
    public class ExecuteProperty
    {
        static void Main(string[] args)
        {
            Propery1 p = new Propery1();
            double res = p.Radius;//get the value by using property
            Console.WriteLine(res);
            p.Radius = 23.22; //set the value by using property
            Console.WriteLine(p.Radius);
        }
    }

}