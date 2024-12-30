using System;
namespace IndexersDemo
{
    class Employee
    {
        int _EmpId;
        string _Name;
        string _Dname;
        double _Salary;
        string _Location;

        public Employee(int empId, string name, string dname, double salary, string location)
        {
            _EmpId = empId;
            _Name = name;
            _Dname = dname;
            _Salary = salary;
            _Location = location;
        }
        public object this[int index]
        {
            get
            {
                if (index == 0)
                    return _EmpId;
                else if (index == 1) return _Name;
                else if (index == 2) return _Dname;
                else if (index == 3) return _Salary;
                else if (index == 4) return _Location;
                else return 0;
            }
            set
            {
                if (index == 0) _EmpId = (int)value;
                else if (index == 1) _Name = (string)value;
                else if (index == 2) _Dname = (string)value;
                else if (index == 3) _Salary = (double)value;
                else if (index == 4) _Location = (string)value;
                else throw new ArgumentException("This Index not available");

            }
        }
    }
}
