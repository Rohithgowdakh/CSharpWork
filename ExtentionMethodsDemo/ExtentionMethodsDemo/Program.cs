using System;

namespace ExtentionMethodsDemo
{
    static public class Extentions
    {
        public static long Factorial(this Int32 num)
        {
            if(num==1)
                return 1;
            if (num == 2) return 2;
            else
                return num * Factorial(num - 1);
        }
        public static string ToProper(this string str)
        {
            string newStr = null;
            if (str.Trim().Length > 0)
            {
                str=str.ToLower();
                string[] arr = str.Split();
                foreach (string s in arr)
                {
                    char[] ch = s.ToCharArray();
                    ch[0] = char.ToUpper(ch[0]);
                    if(newStr==null)
                    newStr =new string(ch);
                    else
                    newStr += " " + new string(ch);
                }
                return newStr;
            }
            return str;
        }
        
      
    }
    class ExtenDemo
    {
        public static void Main(string[] args)
        {
            int i = 5;
            Console.WriteLine(i.Factorial());//this is extention method added to int32 class
            string s = "HEllo mY nAmE is RohiTh";
            string proper=s.ToProper();
            Console.WriteLine(proper);
        }
    }
  
}
