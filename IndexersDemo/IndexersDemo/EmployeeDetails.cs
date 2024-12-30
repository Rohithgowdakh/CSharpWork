using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDemo
{
    public class EmployeeDetails
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee(101, "Manu", "Sales", 50000.00, "Bengaluru");
            Console.WriteLine("Employee Id :" + emp[0]);
            Console.WriteLine("Employee Name :" + emp[1]);
            Console.WriteLine("Department Name: " + emp[2]);
            Console.WriteLine("Employeee Salary :"+emp[3]);
            Console.WriteLine("Employee Location :" + emp[4]);


            Console.WriteLine();
            emp[1] += " D P ";
            emp[2] = "Data Science";
            Console.WriteLine("Employee Id :" + emp[0]);
            Console.WriteLine("Employee Name :" + emp[1]);
            Console.WriteLine("Department Name: " + emp[2]);
            Console.WriteLine("Employeee Salary :" + emp[3]);
            Console.WriteLine("Employee Location :" + emp[4]);
        }
    }
}
