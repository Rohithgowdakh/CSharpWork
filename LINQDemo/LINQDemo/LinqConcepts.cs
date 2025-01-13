using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class LinqConcepts
    {
        //Distinct numbers from list
        public static void DistinctNum()
        {
            var numbers= new List<int>() { 1,2,3,4,4,5,2,2,1,6,7,3};
            var distinctNum = numbers.Distinct();
        }
        public static void Main(string[] args)
        {
            DistinctNum();
        }
    }
}
