using System;
namespace Polymorphism
{
    public abstract class Figures
    {
        public double radius, length, breadth;
        public float pi = 3.14f;
        public abstract double getArea();
    }
    class Rectangle : Figures
    {
        public Rectangle(double length, double breadth)
        {
            this.length = length;
            this.breadth = breadth;
        }
        public override double getArea()
        {
            return length*breadth;
        }
    }
    class Square : Figures
    {
        public Square(double length)
        {
            this.length = length;
        }
        public override double getArea()
        {
            return length * length;
        }
    }
    class Circle : Figures
    {
        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double getArea()
        {
            return pi*radius*radius;
        }
    }
    class TestFigures
    {
        static void Main(string[] args)
        {
            Figures fig;
            fig = new Rectangle(10.22, 20.32);
            Console.WriteLine("Area of Rectangle :" + fig.getArea());
            fig = new Square(10);
            Console.WriteLine("Area of Square :" + fig.getArea());
            fig = new Circle(11.11);
            Console.WriteLine("Area of Circle :" + fig.getArea());
            Console.ReadLine();
        }
    }
}