using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class Joins
    {
        static DataInitializing dataInit = new DataInitializing();
        public static void InnerJoin()
        {
            var employee = dataInit.EmployeeData();
            var department = dataInit.DepartmentData();
            var data = employee.Join(department, e => e.Id, d => d.DNo, (e, d) => new { EmployeeName = e.Name, DepartmentName = d.DeptName });
            foreach (var item in data)
            {
                Console.WriteLine(item.EmployeeName+"--"+item.DepartmentName);
            }
        }
        public static void GroupedJoin()
        {
            var employee = dataInit.EmployeeData();
            var department = dataInit.DepartmentData();
            var data = from e in employee
                       join d in department
                       on e.Id equals d.DNo into groupedEmployee
                       select
                       new
                       {
                           EmployeeName=e.Name,
                           Department=groupedEmployee
                       };
            foreach (var item in data)
            {
                Console.WriteLine(item.EmployeeName);
                foreach(var g in item.Department)
                {
                    Console.WriteLine($"      {g.DeptName}");
                }
            }
        }
        public static void LeftJoin()
        {
            var employee = dataInit.EmployeeData();
            var department = dataInit.DepartmentData();
            var data = from e in employee
                       join d in department
                       on e.Id equals d.DNo into groupedEmployee
                       from ge in groupedEmployee.DefaultIfEmpty()
                       select 
                       new { 
                           EmployeeName = e.Name,
                           DepartmentName = ge == null ? "no matching department" : ge.DeptName 
                       };
            foreach (var item in data)
            {
                Console.WriteLine(item.EmployeeName + "--" + item.DepartmentName);
            }
        }
        public static void Queries()
        {
            var employee = dataInit.EmployeeData();
            var department = dataInit.DepartmentData();

            var NameGT6FindAll = employee.FindAll(em=>em.Name.Length<6).ToList();
            foreach (var item in NameGT6FindAll) { Console.WriteLine(item.Name); }

            Console.WriteLine("------------------------------------------");
            var NameGT6Find = employee.Find(em => em.Name.Length < 6);
            Console.WriteLine(NameGT6Find.Name);

            Console.WriteLine("------------------------------------------");
            var name = employee.Where(e=>e.Address=="Bengaluru").ToList();
            foreach (var item in name) { Console.WriteLine(item.Name); }

            Console.WriteLine("------------------------------------------");
            var name1 = from e in employee
                        where e.Address == "Netherland"
                        select e.Name;
            foreach (var item in name1){Console.WriteLine(item); }

            Console.WriteLine("------------------------------------------");

        }
        public static void Main(string[] args)
        {
            //InnerJoin(); 
            //LeftJoin();
            //GroupedJoin();
            Queries();  
        }
    }
}
