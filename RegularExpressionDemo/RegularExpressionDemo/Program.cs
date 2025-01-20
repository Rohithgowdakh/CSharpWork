using System;
using System.Text.RegularExpressions;
namespace RegularExpressionDemo

{
    public class RegexDemo
    {
        public static void RegexForEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9.%&_+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(email);
            bool bb = regex.IsMatch(email);
            Console.WriteLine(bb);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        public static void RegexForNumber(long number)
        {
            string pattern = @"^\+?([1-9]\d{0,2})?[.-\s]?\(?\d{1-4}\)?[.-\s]?\d{1,4}[-.\s]?\d{1-9}$";

            Regex regex = new Regex(pattern);
            MatchCollection match = regex.Matches(pattern);
            foreach (var item in match)
            {
                Console.WriteLine(match);
            }
        }
        static void Main(string[] args)
        {
            string email = "rohithkh2024@gmail.com";
            RegexForEmail(email);
            long num = +91 - 123 - 4567;
            RegexForNumber(num);
        }
    }
}
