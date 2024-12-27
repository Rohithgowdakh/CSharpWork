using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDemo
{
    public enum Cities
    {
        Bengaluru,Mumbai,Delhi,Hyderbad,Kolkatha,Pune
    }
    public class Customer
    {
        int _CustID;
        bool _Status;
        string _CustName;
        double _Balance;
        Cities _City;
        string _State;
        public string Country { get; } = "India";
        public Customer(int  CustID, bool Status,string CustName,double Balance , Cities City,string State)
        {
            _CustID = CustID;
            _Status = Status;
            _CustName = CustName;
            _Balance = Balance;
            _City = City;
            _State = State;
        }
        public int CustID
        {
            get { return _CustID; }
        }
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string CustName
        {
            get { return _CustName; }
            set
            {
                if (Status == true)
                    _CustName = value;
            }
        }
        public double Balance
        {
            get { return _Balance; }
            set
            {
                if (Status == true)
                {
                    if(value >=500)
                    _Balance = value;
                }
                
            }
        }
        public string State
        {
            get { return _State; }

            protected set
            {
                if (Status == true)
                    _State = value;
            }
        }
        public Cities City
        {
            get { return _City; }
            set
            {
                if (Status == true)
                    _City = value;
            }
        }
    }
    public class CustomerData
    {
        static void Main(string[] args)
        {
            Customer cus = new Customer(101,false,"Rohith",5000,Cities.Bengaluru,"Karnataka");
            Console.WriteLine("Customer Id :"+cus.CustID);
            //cus.CustID = 102; we cant set value becouse we applay only get function for this
            if(cus.Status == true) 
                Console.WriteLine("Customer Status : Active");
            else
                Console.WriteLine("Customer Status :In-Active");
            Console.WriteLine("Customer Name :"+cus.CustName);
            cus.CustName += " K H";
            Console.WriteLine("Modified Name :" + cus.CustName);
            Console.WriteLine("Customer Balance :" + cus.Balance);
            cus.Balance -= 3000;
            Console.WriteLine("Modified Balance :" + cus.Balance);
            cus.Status = true;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAfter Status Becomes True");
            cus.CustName += "K H";
            Console.WriteLine("Modified Name :" + cus.CustName);
            cus.Balance -= 3000;
            Console.WriteLine("Modified Balance :" + cus.Balance);
           
            cus.Balance -= 1600;//Modification failed ,so value remains 2000
            Console.WriteLine("Try to modify balance to below 500 :"+cus.Balance);

            Console.WriteLine("City Name :"+cus.City);
            cus.City = Cities.Kolkatha;
            Console.WriteLine("Modified City :"+cus.City);
            Console.WriteLine("State Name :"+cus.State);
            //cus.State = "Telangana"; not possible becouse set is protected here
            Console.WriteLine("Country Name :" + cus.Country);
            //cus.Country = "Aus"; here defined new approach with only get ,so can't set
            Console.ReadLine();
        }
    }
}
