using System;
namespace GroceryManagement
{
    public class Items
    {

        public static void Main(string[] args)
        {
            GroceryUtility items = new GroceryUtility();
            items.InitializeItems();
            bool notExit = true;
            while (notExit)
            {
                Console.WriteLine("Grocery Management App");
                Console.WriteLine("1. Display Items");
                Console.WriteLine("2. Generate Bill for Customer");
                Console.WriteLine("3. Check Sales Data");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter Your Choise :");

                string input = Console.ReadLine();
                int choice = int.Parse(input);
                if (choice > 0 && choice < 5)
                {
                    switch (choice)
                    {
                        case 1:
                            items.DisplayItems();
                            break;
                        case 2:
                            items.addAnother();
                            break;
                        case 3:
                            items.SaledData();
                            break;
                        case 4:
                            Console.WriteLine("Exiting the Apllication.");
                            notExit = false;
                            break;
                    }
                }
                else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); }
            }
        }
        

    }
}

