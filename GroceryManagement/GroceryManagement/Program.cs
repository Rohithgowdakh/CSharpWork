using System;
namespace GroceryManagement
{
    public class Items
    {

        public static void Main(string[] args)
        {
            GroceryUtility items = new GroceryUtility();
            items.InitializeItems();
            items.FinalizeInvoice();
        }
        

    }
}

