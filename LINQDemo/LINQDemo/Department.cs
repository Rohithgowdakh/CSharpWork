using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class Department
    {
        public int DNo { get; set; }
        public string DeptName {  get; set; }
        public string Location {  get; set; }
        public Department(int dNo, string deptName,string location)
        {
            DNo = dNo;
            DeptName = deptName;
            Location = location;
        }
    }
}
