using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDemo
{
    public class EmployeeIndexersStringDetails
    {
        static void Main(string[] args)
        {
            EmployeeIndexerString emp = new EmployeeIndexerString(101, "Manu", "Sales", 50000.00, "Bengaluru");
            Console.WriteLine("Employee Id :" + emp["eid"]);
            Console.WriteLine("Employee Name :" + emp["name"]);
            Console.WriteLine("Department Name: " + emp["dname"]);
            Console.WriteLine("Employeee Salary :" + emp["salary"]);
            Console.WriteLine("Employee Location :" + emp["location"]);


            Console.WriteLine();
            emp["name"] += " D P ";
            emp["dname"] = "Data Science";
            Console.WriteLine("Employee Id :" + emp["eid"]);
            Console.WriteLine("Employee Name :" + emp["name"]);
            Console.WriteLine("Department Name: " + emp["dname"]);
            Console.WriteLine("Employeee Salary :" + emp["salary"]);
            Console.WriteLine("Employee Location :" + emp["location"]);
        }
    }
}
