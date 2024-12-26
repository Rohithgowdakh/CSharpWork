using System;
namespace OperatorOverload
{
    public class Matrix
    {
        int a, b, c, d;
        public Matrix(int a, int b, int c, int d)
        {

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            Matrix m = new Matrix(mat1.a + mat2.a, mat1.b + mat2.b, mat1.c + mat2.c, mat1.d + mat2.d);

            return m;
        }
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            Matrix m = new Matrix(mat1.a - mat2.a, mat1.b - mat2.b, mat1.c - mat2.c, mat1.d - mat2.d);

            return m;
        }
        public override string ToString()
        {
            return a+" "+b+"\n"+c+" "+d+"\n";
        }

        static void Main(string[] args)
        {
            
            Matrix m1 = new Matrix(10, 15, 20, 25);
            Matrix m2=new Matrix(10,15, 20, 25);
            Matrix m3 = m1 + m2;
            Matrix m4 = m1 - m2;

            Console.WriteLine(m1);
            Console.WriteLine(m2);
            Console.WriteLine(m3);
            Console.WriteLine(m4);

            Console.ReadLine();

        }
    }
}