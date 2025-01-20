using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class DataInitializing
    {
        List<Employee> _employee=new List<Employee>();
        List<Department> _department=new List<Department>();
        public List<Employee> EmployeeData()
        {
            _employee.Add(new Employee(2, "Virat", "Bengaluru", 9889282928));
            _employee.Add(new Employee(4, "Salt", "Netherland", 9737739925));
            _employee.Add(new Employee(3, "Patidar", "USA", 9889653928));
            _employee.Add(new Employee(5, "Bethel", "Argentina", 5569282928));
            _employee.Add(new Employee(7, "Jithesh Sharma", "Malaysia", 9889243263));
            _employee.Add(new Employee(7, "Swapnil Singh", "Malaysia", 9889243263));
            _employee.Add(new Employee(5, "Romario Shephard", "West Indies", 9889243263));
            _employee.Add(new Employee(9, "Bhuvaneshwar Kumar", "Hyderbad", 9889243263));
            _employee.Add(new Employee(8, "Yash Dayal", "Kolkatha", 9889243263));
            _employee.Add(new Employee(11, "Krunal Pandya", "Mumbai", 9889243263));
            return _employee;
        }

        public List<Department> DepartmentData()
        {
            _department.Add(new Department(2, "Finance", "Bengaluru"));
            _department.Add(new Department(1, "IT", "UAE"));
            _department.Add(new Department(4, "Sales", "Netherland"));
            _department.Add(new Department(5, "IT", "Argentina"));
            _department.Add(new Department(7, "Marketing", "Malaysia"));
            return _department;
        }
    }
}
