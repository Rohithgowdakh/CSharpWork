using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    public class DictionaryDemo
    {
        public int Eid {  get; set; }

        public string Name {  get; set; }
        public int Age {  get; set; }

        public DictionaryDemo(int eid, string name,int age)
        {
            Eid = eid; Name = name;Age = age;
        }

       public static void Main(string[] args)
        {
            Dictionary<string, DictionaryDemo> demo = new Dictionary<string, DictionaryDemo> ();
            DictionaryDemo d1 = new DictionaryDemo(100, "Manu", 21);
            DictionaryDemo d2 = new DictionaryDemo(101, "Sai", 22);
            DictionaryDemo d3 = new DictionaryDemo(102, "Rohith", 21);
            DictionaryDemo d4 = new DictionaryDemo(103, "Kiran", 25);

            demo[d1.Name] = d1;
            demo[d2.Name] = d2;
            demo[d3.Name] = d3;
            demo[d4.Name] = d4;
            foreach(KeyValuePair<string , DictionaryDemo> dd in demo)       
            {
                Console.WriteLine(dd.Value.Eid+" "+dd.Key+" "+dd.Value.Age);
            }

        }
       
    }
}
