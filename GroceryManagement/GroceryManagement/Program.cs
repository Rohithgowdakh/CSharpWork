using System;
namespace GroceryManagement
{
    public struct Items
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Prize { get; set; }
        public Items(string name, int quantity, double prize)
        {
            Name = name;
            Quantity = quantity;
            Prize = prize;
        }
        public static void Main(string[] args)
        {
            InitializeItems();
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
                            DisplayItems();
                            break;
                        case 2:
                            GenerateBill();
                            break;
                        case 3:
                            SaledData();
                            break;
                        case 4:
                            Console.WriteLine("Exiting the Apllication.");
                            notExit = false;
                            break;
                    }
                }
                else { Console.WriteLine("Invalid Input Please try again"); }

            }                    
        }
        static Dictionary<string, Items> items = new Dictionary<string, Items>();
        static Dictionary<string,int> saledItems = new Dictionary<string, int>();
        public static void InitializeItems()
        {
            items["Mango"] = new Items("Mango", 100, 150.00);
            items["Rice"] = new Items("Rice", 200, 34.00);
            items["Apple"] = new Items("Apple", 300, 250.00);
            items["Banana"] = new Items("Banana", 70, 51.00);
            items["Wheat"] = new Items("Wheat", 700, 85.00);
            Console.WriteLine();
        }
        public static void DisplayItems()
        {    
            
            foreach (KeyValuePair<string, Items> item in items)
            {
                Console.BackgroundColor=ConsoleColor.Green; 
                Console.WriteLine("Name :"+item.Key+" quantity:"+item.Value.Quantity+" Price :"+item.Value.Prize);
                
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        public static void GenerateBill()
        {
            Console.WriteLine("Enter Item Name :");
            string name=Console.ReadLine();
            if(!items.ContainsKey(name))
            {
                Console.WriteLine("This Item Not Available");
            }
            else
            {
                Items i1 = items[name];
                Console.WriteLine($"Enter Quantity for {i1.Name}");
                string squantity=Console.ReadLine();
                int quantity=int.Parse(squantity);
                if (i1.Quantity <= 0) { Console.WriteLine("item is sold out"); return; }
                if (quantity > i1.Quantity ) { Console.WriteLine("Insufficient Stock "); return; }
                
                i1.Quantity -= quantity;
                if (!saledItems.ContainsKey(name))
                {
                    saledItems[name] = quantity;
                }
                else
                {
                    saledItems[name] += quantity;
                }
                Console.WriteLine($"name :{i1.Name}\nQuantity :{quantity} \nPrize :{i1.Prize*quantity}");
                Console.WriteLine();
            }
        }
        public static void SaledData()
        {
            
            foreach (KeyValuePair<string,int> data in saledItems)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("name :"+data.Key+" Sold :"+data.Value);
            }
            Console.WriteLine();
            foreach (KeyValuePair<string, Items> item in items)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Name :" + item.Key + " quantity:" + item.Value.Quantity);

            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}

