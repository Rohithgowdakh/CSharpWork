using System;
using System.Collections;
using System.Collections.Generic;


namespace CollectionsDemo
{
    public class HashTableDemo
    {
        public void HashTableWithoutGeneric()
        {
            Hashtable h1 = new Hashtable();
            h1.Add("Eid", 1001);
            h1.Add("Name", "Manu");
            h1.Add("Job", "Developer");

            h1.Add(1, 8757789759);
            foreach (object item in h1.Keys)
            {
                Console.WriteLine(item+ " : " + h1[item]);
            }
        }
      }
}
