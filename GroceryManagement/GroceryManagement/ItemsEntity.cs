using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManagement
{
    public class ItemsEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ItemsEntity(string name, int quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
        public ItemsEntity() { }
    }
}
