using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StockExchange
{
    class Program
    {
        public static void Main(string[] args)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.InitializeAllTables();
            
            while (true)
            {
                Console.WriteLine("\n1. Registration\n2. Login\n3. Exit");
                Console.WriteLine("Choose an option :");
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1: loginPage.RegisterUser(); break;
                        case 2: loginPage.LoginUser(); break;
                        case 3: Console.WriteLine("Exiting application. Goodbye!"); return;
                        default: Console.WriteLine("Invalid option. Try again."); break;
                    }
                }
                else Console.WriteLine("Please enter a valid option");

            }
        }
    }
}
