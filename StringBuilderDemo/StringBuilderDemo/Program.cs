using System;
using System.Text;
namespace StringBuilderDemo
{
    public class StringsDemo
    {
        public static void StringBuliderDemo()
        {
            string sb1 = "South";
            Console.WriteLine(sb1);
            //Concate string
            StringBuilder sb = new StringBuilder(sb1);
            sb.Append(" Africa");
            Console.WriteLine(sb);
            //delete string
            sb.Remove(5,7);
            Console.WriteLine(sb);
            //insert new string
            sb.Insert(0,"North ");
            Console.WriteLine(sb);
            //Replace new String with old one
            sb.Replace("South", "India");
            Console.WriteLine(sb);
            string ns=sb.ToString();
            Console.WriteLine(ns);
        }
        public static void Main(string[] args)
        {
            StringBuliderDemo();
        }
    }
}
