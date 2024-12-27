using System;
using System.Globalization;
namespace EnumerationDemo
{
    public enum Days
    {
        Monday=1,
        Tuesday,
        Wednesday=13,
        Thursday,
        Friday
    }
   
      
    
    class ExecuteEnum
    {
        public Days meetingDate { get; set; } = 0;
      
     
        public static Days meetingDay { get; set; }=Days.Monday;
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;   
            Days d =(Days)2;
            Console.WriteLine(d);
            Days d1=Days.Wednesday;
            Console.WriteLine((int)d1);
            Console.BackgroundColor = ConsoleColor.Blue;
            foreach (int i in Enum.GetValues(typeof(Days)))
            {
                Console.WriteLine(i+" : "+(Days)i);
            }
            Console.BackgroundColor = ConsoleColor.Cyan;
            foreach(String s in  Enum.GetNames(typeof(Days)))
            {
                Console.WriteLine(s);
            }
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine(meetingDay);
            meetingDay = Days.Wednesday;
            Console.WriteLine(meetingDay);
            Console.ReadLine();
        }    
    }
}