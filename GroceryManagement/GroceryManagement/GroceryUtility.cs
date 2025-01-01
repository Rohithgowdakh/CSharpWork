using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManagement
{
    public class GroceryUtility
    {
         Dictionary<string, ItemsEntity> _items = new Dictionary<string, ItemsEntity>();
         Dictionary<string, int> _saledItems = new Dictionary<string, int>();
         Dictionary<string , ItemsEntity> _total=new Dictionary<string, ItemsEntity> ();
        double totalPrice = 0;
        public  void InitializeItems()
        {
            try
            {
                _items["Mango".ToLower()] = new ItemsEntity("Mango", 100, 150.00);
                _items["Rice".ToLower()] = new ItemsEntity("Rice", 200, 34.00);
                _items["Apple".ToLower()] = new ItemsEntity("Apple", 300, 250.00);
                _items["Banana".ToLower()] = new ItemsEntity("Banana", 70, 51.00);
                _items["Wheat".ToLower()] = new ItemsEntity("Wheat", 700, 85.00);
                Console.WriteLine();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public  void DisplayItems()
        {
            try
            {
                foreach (KeyValuePair<string, ItemsEntity> item in _items)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("Name :" + item.Key + " quantity:" + item.Value.Quantity + " Price :" + item.Value.Price);

                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public  void GenerateBill()
        {
            try
            {
                Console.WriteLine("Enter Item Name :");
                string name = Console.ReadLine();
                name=name.ToLower();
                if (!_items.ContainsKey(name))
                {
                    Console.WriteLine("This Item Not Available");
                    return;
                }
                else
                {

                    ItemsEntity item = _items[name];
                    Console.WriteLine($"Enter Quantity for {item.Name}");
                    string squantity = Console.ReadLine();
                    int quantity = int.Parse(squantity);
                    if (item.Quantity <= 0) { Console.WriteLine("item is sold out"); Console.WriteLine(); return; }
                    if (quantity > item.Quantity) { Console.WriteLine("Insufficient Stock "); Console.WriteLine(); return; }

                    item.Quantity -= quantity;
                    if (!_saledItems.ContainsKey(name)){ _saledItems[name] = quantity;}
                    else
                    {
                        _saledItems[name] += quantity;
                    }
                    //Console.WriteLine($"name :{item.Name}\nQuantity :{quantity} \nPrize :{item.Price * quantity}");

                    if (!_total.ContainsKey(name))
                    {
                        _total[name] = new ItemsEntity(name, quantity, item.Price * quantity);
                        totalPrice += (item.Price * quantity);

                        //Console.WriteLine($"name :{item.Name}\nQuantity :{quantity} \nPrize :{item.Price * quantity}");
                        Console.WriteLine();

                    }
                    else
                    {

                        _total[name].Quantity += quantity;
                        _total[name].Price = item.Price * _total[name].Quantity;
                        totalPrice += (item.Price * quantity);

                        //Console.WriteLine($"name :{item.Name}\nQuantity :{item.Quantity += quantity} \nPrize :{item.Price * quantity}");

                    }
                    
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public  void SaledData()
        {
            try
            {
                foreach (KeyValuePair<string, int> data in _saledItems)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("name :" + data.Key + " Sold :" + data.Value);
                }
                Console.WriteLine();
                Console.WriteLine("---Available Items---");
                Console.WriteLine();
                foreach (KeyValuePair<string, ItemsEntity> item in _items)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("Name :" + item.Key + " quantity:" + item.Value.Quantity);

                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void addAnother()
        {
            try
            {
                GenerateBill();
                Console.WriteLine();
                bool IsSelect = true;
                while (IsSelect)
                {
                    Console.WriteLine("1. Add Another Item");
                    Console.WriteLine("2. Remove Item");
                    Console.WriteLine("3. Exit");
                    Console.WriteLine();
                    Console.WriteLine("Do You Need Another Item :");
                    string add = Console.ReadLine();
                    int select = int.Parse(add);

                    switch (select)
                    {
                        case 1:
                            GenerateBill();
                            break;
                        case 2:
                            RemoveItem();
                            break;

                        case 3:
                            IsSelect = false;
                            totalAmount();
                            Console.WriteLine();
                            Console.WriteLine("        Thank You...!");
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("invalid option , Choose 1,2 or 3");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void totalAmount()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Name :   Quantity :         Price :");

                foreach (KeyValuePair<string, ItemsEntity> totalItems in _total)
                {

                    Console.WriteLine($"{totalItems.Key}    {totalItems.Value.Quantity}                 {totalItems.Value.Price}  ");
                    Console.WriteLine();

                }
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Total Amount :              " + totalPrice);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public void RemoveItem()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Enter Item Name to Remove :");
                string name = Console.ReadLine();
                name=name.ToLower();
                if (!_total.ContainsKey(name))
                {
                    Console.WriteLine("This Item is not in the bill");
                }
                else
                {
                    Console.WriteLine($"Enter the quantity to remove for {name}");
                    string quantityS = Console.ReadLine();
                    int removeQuantity = int.Parse(quantityS);
                    if (removeQuantity <= 0)
                    {
                        Console.WriteLine("Enter the valid quantity");

                    }
                    if (removeQuantity > _total[name].Quantity)
                    {
                        Console.WriteLine("Cannot Remove more then what was added");
                    }
                    else
                    {
                        _total[name].Quantity -= removeQuantity;
                        _total[name].Price -= removeQuantity * _items[name].Price;
                        totalPrice-= removeQuantity * _items[name].Price;
                        _saledItems[name] -= removeQuantity;
                    }
                    if (_total[name].Quantity == 0)
                    {
                        _total.Remove(name);
                       
                    }

                    _items[name].Quantity += removeQuantity;
                }
                totalAmount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
