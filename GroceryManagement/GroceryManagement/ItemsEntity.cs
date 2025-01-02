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
        public double Quantity { get; set; }
        public double Price { get; set; }

        public string ID { get; set; }
        public ItemsEntity(string name, double quantity, double price,string id)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            ID = id;
        }
        public ItemsEntity() { }
    }
}
