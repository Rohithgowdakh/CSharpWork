using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelrgatesDemo
{
    public class MultiCast
    {
        public delegate void RectDelegate(double width,double height);
        public static void GetArea(double width,double height)
        {
            Console.WriteLine("Area of Rectangle :"+(width*height));
        }
        public void GetParemeter(double width,double height)
        {
            Console.WriteLine("Paremeter of Rectangle :"+2*(width+height));
        }
        public static void Main(string[] args)
        {
            MultiCast m=new MultiCast();
            RectDelegate r1 = GetArea;//here we directly pass the method as a parameter to delegate, same as below
            //RectDelegate r1 =new RectDelegate(GetArea);
            r1 += m.GetParemeter;//multicast means execute multiple method in single call which have same return type and parameters

            r1.Invoke(20.34, 44.54);

        }
    }
}
