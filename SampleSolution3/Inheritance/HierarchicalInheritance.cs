using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class HierarchicalInheritance
    {
        public void AreaofRectangle(int length,int breadth)
        {
            Console.WriteLine("Area of Rectangle :" + length * breadth);
        }
    }
    class Area2 : HierarchicalInheritance
    {
        public void AreaofSquare(int side)
        {
            Console.WriteLine("Area of Square :" + side * side);
        }
    }
    class Area3 : HierarchicalInheritance
    {
        public void AreaofCircle(int radius)
        {
            Console.WriteLine("Area of Circle :" +Math.PI* radius * radius);
        }
        static void Main(string[] args)
        {
            Area2 area = new Area2();
            area.AreaofSquare(2);
            area.AreaofRectangle(2, 2);
            Area3 area3 = new Area3();
            area3.AreaofCircle(2);  
            area3.AreaofRectangle(2, 2);
        }
    }
}
