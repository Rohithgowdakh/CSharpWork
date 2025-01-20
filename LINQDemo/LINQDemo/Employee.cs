using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address {  get; set; }
        public long Contact {  get; set; }
        public Employee(int id,string name,string address,long contact) {
            Id = id;
            Name = name;
            Address = address;
            Contact = contact;
        }
    }
}
