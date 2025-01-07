using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelTicketBooking
{
    public class TravelUtility
    {
        List<string> _acBuses = new List<string>() { "rajahamsa","vrl","airavatha"};
        List<string> _nonAcBuses = new List<string>() { "ksrtc","bmtc","tci" };
        List<string> _location = new List<string>() { "hassan", "mandya", "bengaluru", "thumakuru", "hubballi", "udupi", "ballary" };
        bool login = true;
        public void LoginPage()
        {
            try
            {
               
                while (login)
                {
                    
                    //Console.WriteLine(bb);
                    Console.WriteLine("Enter Admin Id :");
                    string userId = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password :");
                    string password = Console.ReadLine();
                    if (userId == "Rohith" && password == "Rohith2004")
                    {
                       
                        Console.WriteLine("Login Successfull...!");
                        Console.WriteLine();
                        Services();
                    }
                    else
                    {
                        Console.WriteLine("User Id and Password Mismatch, Enter Correct User ID and Password");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Services()
        {
            bool service = true;
            while (service)
            {
                Console.WriteLine("Select The Service :");
                Console.WriteLine("1. Book a Ticket");
                Console.WriteLine("2. Cancel Ticket");
                Console.WriteLine("3. Check Booking Status");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter Your Choice :");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice > 0 && choice < 5)
                    {
                        switch (choice)
                        {
                            case 1: TicketBooking(); break;
                            case 2: Services(); break;
                            case 3: Services(); break;
                            case 4: Console.WriteLine("Exiting the Application."); service = false; login = false; return;
                        }
                    }
                    else { Console.WriteLine("Enter The Valid Choice"); }

                }
                else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); }
            }
        }
        public void TicketBooking()
        {
            Console.WriteLine("Select Avilable Bus Facility :");
            Console.WriteLine("1. Book AC");
            Console.WriteLine("2. Book Non AC");
            Console.WriteLine();
            Console.WriteLine("Enter Your Choice :");
            string input = Console.ReadLine();
            int choice;
            if (int.TryParse(input, out choice))
            {
                if (choice>0 && choice<3)
                {
                    switch (choice)
                    {
                        case 1: ACBusBooking(); break;
                        case 2: NonAcBooking(); break;
                    }
                }
                else { Console.WriteLine("Enter a Valid Choice..");Console.WriteLine(); return; }
            }
            else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); return; }
        }
        public void ACBusBooking()
        {
            Console.WriteLine("Enter From Location :");
            string fromLocation = Console.ReadLine();
            fromLocation=fromLocation.ToLower();
            
            
                if ( _location.Contains(fromLocation))
                {
                    Console.WriteLine("Enter Destination Location :");
                    string toLocation = Console.ReadLine();
                    toLocation = toLocation.ToLower();
                    if (_location.Contains(toLocation))
                    {
                    //Console.WriteLine("Enter Date(dd-mm-yyyy) of Travel :");

                        Console.WriteLine("Buses Available For This Location");
                        Console.WriteLine();
                            foreach (string buses in _acBuses)
                            {
                                Console.WriteLine(buses);
                            }
                         Console.WriteLine("Enter The Bus Name You Wish To Journey :");
                        Console.WriteLine();
                        string busName= Console.ReadLine();
                        busName = busName.ToLower();
                        if (_acBuses.Contains(busName))
                        {
                            Console.WriteLine("Confirm the Ticket, Will You Ready For The Jorney (Y/N)");
                            Console.WriteLine();
                            string confirmation= Console.ReadLine().ToLower();
                            bool confirmationConfirmed = true;
                            while (confirmationConfirmed)
                            {
                                if (confirmation == "y")
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Final Ticket");
                                    Console.WriteLine();
                                    Console.WriteLine("From : " + fromLocation);
                                    Console.WriteLine();
                                    Console.WriteLine("To : " + toLocation);
                                    Console.WriteLine();
                                    Console.WriteLine("AC Status : Enabled");
                                    Console.WriteLine();
                                    Console.WriteLine("BusName : " + busName);
                                    Console.WriteLine();
                                    Console.WriteLine("Thank You For Booking...");
                                    Console.WriteLine();
                                    confirmationConfirmed=false;
                                }
                                else if (confirmation == "n")
                                {
                                    Console.WriteLine("Your Ticket Cancelled");
                                    confirmationConfirmed = false;
                                }
                                else
                                {
                                    Console.WriteLine("Enter Y or N");
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.WriteLine("please choose the available buses..");
                        }

                    }
                    else { Console.WriteLine($"For {toLocation} Location Transportaion Not Available");return; }
                }
                else { Console.WriteLine($"For {fromLocation} Location Transportaion Not Available"); return; }
            }
            
        public void NonAcBooking()
        {
            Console.WriteLine("Enter From Location :");
            string fromLocation = Console.ReadLine();
            fromLocation = fromLocation.ToLower();


            if (_location.Contains(fromLocation))
            {
                Console.WriteLine("Enter Destination Location :");
                string toLocation = Console.ReadLine();
                toLocation = toLocation.ToLower();
                if (_location.Contains(toLocation))
                {
                    //Console.WriteLine("Enter Date(dd-mm-yyyy) of Travel :");

                    Console.WriteLine("Buses Available For This Location");
                    Console.WriteLine();
                    foreach (string buses in _nonAcBuses)
                    {
                        Console.WriteLine(buses);
                    }
                    Console.WriteLine("Enter The Bus Name You Wish To Journey :");
                    Console.WriteLine();
                    string busName = Console.ReadLine();
                    busName = busName.ToLower();
                    if (_acBuses.Contains(busName))
                    {
                        Console.WriteLine("Confirm the Ticket, Will You Ready For The Journey (Y/N)");
                        Console.WriteLine();
                        string confirmation = Console.ReadLine().ToLower();
                        bool confirmationConfirmed = true;
                        while (confirmationConfirmed)
                        {
                            if (confirmation == "y")
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.WriteLine("Final Ticket");
                                Console.WriteLine();
                                Console.WriteLine("From : " + fromLocation);
                                Console.WriteLine();
                                Console.WriteLine("To : " + toLocation);
                                Console.WriteLine();
                                Console.WriteLine("BusName : " + busName);
                                Console.WriteLine();
                                Console.WriteLine("AC Status : Disabled");
                                Console.WriteLine();
                                Console.WriteLine("Thank You For Booking...");
                                Console.WriteLine();
                                confirmationConfirmed = false;
                            }
                            else if (confirmation == "n")
                            {
                                Console.WriteLine("Your Ticket Cancelled");
                                confirmationConfirmed = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter Y or N");
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.WriteLine("please choose the available buses..");
                    }

                }
                else { Console.WriteLine($"For {toLocation} Location Transportaion Not Available"); return; }
            }
            else { Console.WriteLine($"For {fromLocation} Location Transportaion Not Available"); return; }
        }
        }
    
}
