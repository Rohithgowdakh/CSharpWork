using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class DataInitializing
    {
        List<Employee> employee=new List<Employee>();
        List<Department> department=new List<Department>();
        public void EmployeeData()
        {
            employee.Add(new Employee(2, "Virat", "Bengaluru", 9889282928));
            employee.Add(new Employee(4, "Salt", "Netherland", 9737739925));
            employee.Add(new Employee(3, "Patidar", "USA", 9889653928));
            employee.Add(new Employee(5, "Bethel", "Argentina", 5569282928));
            employee.Add(new Employee(7, "Jithesh Sharma", "Malaysia", 9889243263));
            employee.Add(new Employee(7, "Swapnil Singh", "Malaysia", 9889243263));
            employee.Add(new Employee(5, "Romario Shephard", "West Indies", 9889243263));
            employee.Add(new Employee(9, "Bhuvaneshwar Kumar", "Hyderbad", 9889243263));
            employee.Add(new Employee(8, "Yash Dayal", "Kolkatha", 9889243263));
            employee.Add(new Employee(11, "Krunal Pandya", "Mumbai", 9889243263));
        }

        public void DepartmentData()
        {
            department.Add(new Department(2, "Finance", "Bengaluru"));
            department.Add(new Department(1, "IT", "UAE"));
            department.Add(new Department(4, "Sales", "Netherland"));
            department.Add(new Department(5, "IT", "Argentina"));
            department.Add(new Department(7, "Marketing", "Malaysia"));
        }
    }
}
