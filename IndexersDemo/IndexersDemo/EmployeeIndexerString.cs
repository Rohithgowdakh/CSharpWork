using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDemo
{
    public class EmployeeIndexerString
    {
        int _EmpId;
        string _Name;
        string _Dname;
        double _Salary;
        string _Location;

        public EmployeeIndexerString(int empId, string name, string dname, double salary, string location)
        {
            _EmpId = empId;
            _Name = name;
            _Dname = dname;
            _Salary = salary;
            _Location = location;
        }
        public object this[string s]
        {
            get
            {
                if (s == "eid")
                    return _EmpId;
                else if (s == "name") return _Name;
                else if (s == "dname") return _Dname;
                else if (s == "salary") return _Salary;
                else if (s == "location") return _Location;
                else return 0;
            }
            set
            {
                if (s == "eid") _EmpId = (int)value;
                else if (s == "name") _Name = (string)value;
                else if (s == "dname") _Dname = (string)value;
                else if (s == "salary") _Salary = (double)value;
                else if (s == "location") _Location = (string)value;
                else throw new ArgumentException("This Index not available");

            }
        }
    }
}
