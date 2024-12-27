using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDemo
{
     class NormalGetSet
    {
        double _Radius = 12.32;
        public double getRadius()
        {
            return _Radius;
        }
        public void setRadius(double radius)
        {
            _Radius = radius;
        }
    }
    public class Class2
    {
        static void Main(string[] args)
        {
            NormalGetSet n= new NormalGetSet();
            double res= n.getRadius();
            Console.WriteLine(res);
            n.setRadius(24.33);
            Console.WriteLine(n.getRadius());
        }
    }
}
