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
        TicketBookingClass ticketBooking = new TicketBookingClass();
        TicketCancelling ticketCancellation;
        CheckTicketBookings checkTicketBookings;
        bool _login = true;

        public TravelUtility()
        {
            ticketCancellation = new TicketCancelling(ticketBooking);
            checkTicketBookings = new CheckTicketBookings(ticketBooking);
        }
        /// <summary>
        /// Prompts the user for an admin username and password, authenticates the login, and initializes services if successful.
        /// </summary>
        public void LoginPage()
        {
            try
            {
                while (_login)
                {
                    
                    string userName = "Rohith";
                    string password = "Rohith2004";
                    Console.WriteLine("Enter Admin Id :");
                    string userNameTemp = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password :");
                    string passwordTemp = Console.ReadLine();
                    if (userNameTemp == userName && passwordTemp == password)
                    {
                       
                        Console.WriteLine("Login Successfull...!");
                        ticketBooking.InitializeListItems();
                        Console.WriteLine();
                        Services();
                    }
                    else
                    {
                        Console.WriteLine("User Id and Password Mismatch, Enter Correct Admin ID and Password");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Displays the available services (book, cancel, check booking, or exit) and processes the user's choice.
        /// </summary>
        public void Services()
        {
            try
            {
                bool service = true;
                while (service)
                {
                    Console.WriteLine("Select The Service :");
                    Console.WriteLine("1. Book a Ticket");
                    Console.WriteLine("2. Cancel a Ticket");
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
                                case 1:
                                    int flightClass=ticketBooking.SelectFlightClass();
                                    ticketBooking.BookTicket(flightClass);
                                        break;
                                case 2: ticketCancellation.CancelBooking();
                                        break;
                                case 3: checkTicketBookings.CheckBookingStatus();
                                        break;
                                case 4: Console.WriteLine("Exiting the Application.");
                                        service = false; 
                                        _login = false; 
                                        return;
                            }
                        }
                        else { Console.WriteLine("Enter The Valid Choice"); }

                    }
                    else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }   
    }

}
