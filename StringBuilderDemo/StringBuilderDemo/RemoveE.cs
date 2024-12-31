using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBuilderDemo
{
    public class RemoveE
    {
        public static string removeE(string ss)
        { 
            StringBuilder sb = new StringBuilder(ss);
            for (int i = 0; i < sb.Length; i++)
            {
                if ((char)sb[i] =='e'|| (char)sb[i]=='E')
                {
                    sb.Remove(i,1);
                }
            }
            return sb.ToString();
        }
        public static void Main(string[] args)
        {
            string s1 = "AbhcsjbvgctdTCVbkiuChcREwUVuowoeoe ebGCEe hishigyfdWEeweknbkBOiwiepenewvwxeel293eu721$%%82e35e";
            string nn=removeE(s1);
            Console.WriteLine(nn);
        }

    }
}
