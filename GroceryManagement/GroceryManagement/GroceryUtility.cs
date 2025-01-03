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
         Dictionary<string, double> _saledItems = new Dictionary<string, double>();
         Dictionary<string , ItemsEntity> _total=new Dictionary<string, ItemsEntity> ();
         Dictionary<string , ItemsEntity> _createNewInvoice=new Dictionary<string , ItemsEntity> ();
         double totalPrice = 0;
         double currentTotalPrize=0;

        /// <summary>
        /// Initializes the inventory with predefined items and their details.
        /// </summary>
        public void InitializeItems()
        {
            try
            {
                _items["m1".ToLower()] = new ItemsEntity("Mango", 100, 150.00,"m1");
                _items["r1".ToLower()] = new ItemsEntity("Rice", 200, 34.00,"r1");
                _items["a1".ToLower()] = new ItemsEntity("Apple", 300, 250.00,"a1");
                _items["b1".ToLower()] = new ItemsEntity("Banana", 70, 51.00,"b1");
                _items["w1".ToLower()] = new ItemsEntity("Wheat", 700, 85.00,"w1");
                Console.WriteLine();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Manages the main menu of the Grocery Management App, allowing users to display items, 
        /// add items, check sales data, or exit the application.
        /// </summary>
        public void FinalizeInvoice()
        {
            bool notExit = true;
            while (notExit)
            {
                Console.WriteLine("Grocery Management App");
                Console.WriteLine("1. Display Items");
                Console.WriteLine("2. Add Items and Quantity");
                Console.WriteLine("3. Check Sales Data");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter Your Choice :");

                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice > 0 && choice < 5)
                    {
                        switch (choice)
                        {
                            case 1:
                                DisplayItemsFromStock();
                                break;
                            case 2:
                                AddAnotherItem();
                                break;
                            case 3:
                                SaledDataReport();
                                break;
                            case 4:
                                Console.WriteLine("Exiting the Application.");
                                _createNewInvoice.Clear();
                                currentTotalPrize = 0;
                                notExit = false;
                                return;
                            default:
                                Console.WriteLine("Invalid Choice ,Enter a Valid Choice");
                                break;
                        }
                    }
                    else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); }
                }
                else { Console.WriteLine("Enter a valid Choise "); }
            }
        }

        /// <summary>
        /// Displays all items in stock with their details, including ID, name, quantity, and price.
        /// </summary>
        public void DisplayItemsFromStock()
        {
            try
            {
                foreach (KeyValuePair<string, ItemsEntity> item in _items)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("Id :"+item.Key+" Name :" + item.Value.Name + " quantity:" + item.Value.Quantity + " Price :" + item.Value.Price);

                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Processes customer orders by validating item availability, updating stock quantities, 
        /// and generating bill details for purchased items.
        /// </summary>
        public void GenerateBill()
        {
            try
            {
                Console.WriteLine("Enter Item Id :");
                string id = Console.ReadLine();
                id=id.ToLower();
                if (id == "$") { Console.WriteLine("Enter a Valid Item Id"); return; }
                if (!_items.ContainsKey(id))
                {
                    Console.WriteLine("This Item Not Available");
                    return;
                }
                else
                {

                    ItemsEntity item = _items[id];
                    Console.WriteLine($"Enter Quantity for {item.Name}");
                    string squantity = Console.ReadLine();
                    double quantity = double.Parse(squantity);
                    if(quantity==0)
                    {
                        Console.WriteLine("Enter a valid quantity");
                        return;
                    }
                    if (item.Quantity <= 0) { Console.WriteLine("item is sold out"); Console.WriteLine(); return; }
                    if (quantity > item.Quantity) { Console.WriteLine("Insufficient Stock "); Console.WriteLine(); return; }

                    item.Quantity -= quantity;
                    if (!_saledItems.ContainsKey(item.Name)){ _saledItems[item.Name] = quantity;}
                    else
                    {
                        _saledItems[item.Name] += quantity;
                    }
                    //Console.WriteLine($"name :{item.Name}\nQuantity :{quantity} \nPrize :{item.Price * quantity}");

                    if (!_total.ContainsKey(item.Name) ||!_createNewInvoice.ContainsKey(item.Name))
                    {
                        _total[item.Name] = new ItemsEntity(item.Name, quantity, item.Price * quantity,item.ID);
                        _createNewInvoice[item.Name] = new ItemsEntity(item.Name, quantity, item.Price * quantity,item.ID);
                        totalPrice += (item.Price * quantity);
                        currentTotalPrize += (item.Price * quantity);
                        //Console.WriteLine($"name :{item.Name}\nQuantity :{quantity} \nPrize :{item.Price * quantity}");
                        Console.WriteLine();

                    }
                    else
                    {

                        _total[item.Name].Quantity += quantity;
                        _total[item.Name].Price = item.Price * _total[item.Name].Quantity;
                        _createNewInvoice[item.Name].Quantity += quantity;
                        _createNewInvoice[item.Name].Price = item.Price * _total[item.Name].Quantity;
                        totalPrice += (item.Price * quantity);
                        currentTotalPrize += (item.Price * quantity);
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

        /// <summary>
        /// Displays a summary of today's sales, including sold items and total sales amount, 
        /// along with the current available stock details.
        /// </summary>
        public void SaledDataReport()
        {
            try
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Today's Sales Summary and Available Stock");
                Console.WriteLine();
                foreach (KeyValuePair<string, double> data in _saledItems)
                {
                    Console.WriteLine("Name :" + data.Key + " Sold :" + data.Value);
                }
                Console.WriteLine("Total Amount of Today's Sales :" + totalPrice);
                Console.WriteLine();
                Console.WriteLine("---Available Stock---");
                Console.WriteLine();
                foreach (KeyValuePair<string, ItemsEntity> item in _items)
                {
                    Console.WriteLine("Name :" + item.Value.Name + " Quantity:" + item.Value.Quantity);
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Manages the process of adding items, removing items, or generating the final bill 
        /// while providing an interactive menu for the user.
        /// </summary>
        public void AddAnotherItem()
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
                    Console.WriteLine("3. Generate Bill");
                    Console.WriteLine();
                    Console.WriteLine("Enter Your Choice :");
                    string add = Console.ReadLine();
                    int select;
                    if (int.TryParse(add, out select))
                    {
                        switch (select)
                        {
                            case 1:
                                GenerateBill();
                                break;
                            case 2:
                                RemoveItemFromCart();
                                break;

                            case 3:
                                IsSelect = false;
                                TotalAmount();
                                Console.WriteLine();
                                Console.WriteLine("        Thank You...!");
                                Console.WriteLine();
                                _createNewInvoice.Clear();
                                currentTotalPrize = 0;
                                break;
                            default:
                                Console.WriteLine("Invalid option , Choose 1,2 or 3");
                                break;
                        }
                    }
                    else { Console.WriteLine("Enter a Valid Choise"); }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Displays the total amount for the current invoice, showing item details, quantities, and prices.
        /// </summary>
        public void TotalAmount()
        {
            try
            {
                if (_createNewInvoice.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Name :   Quantity :         Price :");

                    foreach (KeyValuePair<string, ItemsEntity> totalItems in _createNewInvoice)
                    {

                        Console.WriteLine($"{totalItems.Key}    {totalItems.Value.Quantity}                 {totalItems.Value.Price}  ");
                        Console.WriteLine();

                    }
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Total Amount :              " + currentTotalPrize);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        /// <summary>
        /// Allows the user to remove an item from the cart by specifying the item ID and quantity,
        /// updates the total amount, and manages the stock and sales data accordingly.
        /// </summary>
        public void RemoveItemFromCart()
        {
            try
            {
                if (_createNewInvoice.Count > 0)
                {
                    Console.WriteLine();
                    
                    Console.WriteLine("Enter Item Id to Remove :");
                    string id = Console.ReadLine();
                    id = id.ToLower();
                    ItemsEntity item = _items[id];
                    if (!_createNewInvoice.ContainsKey(item.Name))
                    {
                        Console.WriteLine("This Item is not in the bill");
                    }
                    else
                    {
                        Console.WriteLine($"Enter the quantity to remove for {item.Name}");
                        string quantityS = Console.ReadLine();
                        double removeQuantity = double.Parse(quantityS);
                        if (removeQuantity <= 0)
                        {
                            Console.WriteLine("Enter the valid quantity");

                        }
                        if (removeQuantity > _createNewInvoice[item.Name].Quantity)
                        {
                            Console.WriteLine("Cannot Remove more then what was added");

                        }
                        else
                        {
                            _createNewInvoice[item.Name].Quantity -= removeQuantity;
                            _createNewInvoice[item.Name].Price -= removeQuantity * _items[id].Price;
                            currentTotalPrize -= removeQuantity * _items[id].Price;
                            totalPrice -= removeQuantity * _items[id].Price;
                            _saledItems[item.Name] -= removeQuantity;

                        }
                        if (_createNewInvoice[item.Name].Quantity == 0)
                        {
                            _createNewInvoice.Remove(item.Name);
                            _saledItems.Remove(item.Name);
                        }

                        _items[id].Quantity += removeQuantity;
                    }
                    TotalAmount();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Cart is Empty , Add Items...");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
       
    }
}
