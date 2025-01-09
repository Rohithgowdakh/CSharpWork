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
        public List<BusEntity> _acBuses = new List<BusEntity>();
        public List<BusEntity> _nonAcBuses = new List<BusEntity>();
        public List<string> _location = new List<string>();
        public List<TravelEntity> _bookings = new List<TravelEntity>();
        
        bool login = true;
        public void InitializeListItems()
        {
            _acBuses.Add(new("vrl travels", 50, 0)); _acBuses.Add(new("sugama tourists", 40, 0));_acBuses.Add(new("airavatha", 70, 0));_acBuses.Add(new("srs travels", 60, 0));_acBuses.Add(new("vayu vajra", 100, 0));
            _nonAcBuses.Add(new("karnataka sarige", 50, 0));_nonAcBuses.Add(new("sarige", 100, 0));_nonAcBuses.Add(new("grameena sarige", 70, 0));_nonAcBuses.Add(new("ambaari non-ac sleeper", 45, 0));
            _location.Add("bagalkot"); _location.Add("ballari"); _location.Add("belagavi"); _location.Add("bengaluru rural"); _location.Add("bengaluru urban"); _location.Add("bidar"); _location.Add("chamarajanagar"); _location.Add("chikballapur"); _location.Add("chikkamagaluru");
            _location.Add("chitradurga"); _location.Add("dakshina kannada"); _location.Add("davanagere"); _location.Add("dharwad"); _location.Add("gadag"); _location.Add("hassan"); _location.Add("haveri"); _location.Add("kalaburagi"); _location.Add("kodagu"); _location.Add("kolar"); _location.Add("koppal");
            _location.Add("mandya"); _location.Add("mysuru"); _location.Add("raichur"); _location.Add("ramanagara"); _location.Add("shivamogga"); _location.Add("tumakuru"); _location.Add("udupi"); _location.Add("uttara kannada"); _location.Add("vijayanagara"); _location.Add("vijayapura"); _location.Add("yadgir");
        }
        public void LoginPage()
        {
            try
            {
                while (login)
                {
                    string UserId = "Rohith";
                    string Password = "Rohith2004";
                    Console.WriteLine("Enter Admin Id :");
                    string userId = Console.ReadLine();
                    
                    Console.WriteLine("Enter Password :");
                    string password = Console.ReadLine();
                    if (userId == UserId && password == Password)
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
                                case 1: TicketBooking(); break;
                                case 2: CancelTicket(); break;
                                case 3: CheckBookingStatus(); break;
                                case 4: Console.WriteLine("Exiting the Application."); service = false; login = false; return;
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
        public void TicketBooking()
        {
            try {
                Console.WriteLine("Select Avilable Bus Facility :");
                Console.WriteLine("1. Book AC");
                Console.WriteLine("2. Book Non AC");
                Console.WriteLine();
                Console.WriteLine("Enter Your Choice :");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice > 0 && choice < 3)
                    {
                        switch (choice)
                        {
                            case 1: ACBusBooking(); break;
                            case 2: NonAcBooking(); break;
                        }
                    }
                    else { Console.WriteLine("Enter a Valid Choice.."); Console.WriteLine(); return; }
                }
                else { Console.WriteLine("Invalid Input Please try again"); Console.WriteLine(); return; }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ACBusBooking()
        {
            try
            {
                bool acBook = true;
                while (acBook)
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
                            if (fromLocation == toLocation) { Console.WriteLine("The destination location must be different from the source location. Please update your entry."); return; }
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine("Enter Date (dd-mm-yyyy) of Travel :");
                                string date = Console.ReadLine();
                                DateTime travelDate;
                                if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out travelDate))
                                {
                                    if (travelDate >= DateTime.Today)
                                    {
                                        for (int b = 0; b < _acBuses.Count; b++)
                                        {
                                            bool isMatchedTravel = false;
                                            foreach (TravelEntity booked in _bookings)
                                            {
                                                string detination = toLocation + " AC Status : Enabled ";
                                                if (booked.Date == travelDate.Date && booked.BusName == _acBuses[b].BusName
                                                    && booked.FromLocation == fromLocation && booked.ToLocation == detination)
                                                {
                                                    isMatchedTravel = true;
                                                    _acBuses[b].FilledSeats = booked.FilledSeats;

                                                }
                                            }
                                            if (!isMatchedTravel){ _acBuses[b].FilledSeats = 0;}
                                        }

                                        Console.WriteLine("Buses Available For This Location");
                                        Console.WriteLine();
                                        for (int buses = 0; buses < _acBuses.Count; buses++)
                                        {
                                            Console.WriteLine($"{buses + 1}. {_acBuses[buses].BusName}\n Available Seats : {_acBuses[buses].AvailableSeats}\n");
                                        }
                                        Console.WriteLine("Select the Bus, You Wish To Journey :\n");
                                        int busIndex = int.Parse(Console.ReadLine());
                                        //for (int booked = 0; booked < _bookings.Count; booked++) { if (_bookings[booked].Date != travelDate && _bookings[booked].BusName == _acBuses[busIndex-1].BusName) { _acBuses[busIndex - 1].FilledSeats = 0;break; } }
                                        if (_acBuses[busIndex - 1].AvailableSeats == 0) { Console.WriteLine($"No available seats on {_acBuses[busIndex - 1].BusName} Bus , Choose another bus"); _acBuses.Remove(_acBuses[busIndex - 1]); return; }
                                        if (busIndex > 0 && busIndex <= _acBuses.Count)
                                        {
                                            Console.WriteLine($"How Many Tickets Would You Like To Book (Maximum {_acBuses[busIndex - 1].AvailableSeats} Tickets):\n");
                                            int ticketsToBook = int.Parse(Console.ReadLine());
                                            if (ticketsToBook > 0 && ticketsToBook <= _acBuses[busIndex - 1].AvailableSeats)
                                            {
                                                bool confirmationConfirmed = true;
                                                while (confirmationConfirmed)
                                                {
                                                    Console.WriteLine("Confirm the Ticket, Are You Ready For The Journey? (Y/N)");
                                                    Console.WriteLine();
                                                    string confirmation = Console.ReadLine().ToLower();
                                                    if (confirmation == "y")
                                                    {
                                                        Console.BackgroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"Final Ticket\n\nFrom :  + {fromLocation}\n\nTo :  + {toLocation}\n\nAC Status : Enabled\n\nBusName :  + {_acBuses[busIndex - 1].BusName}\n\nBooked Tickets Count : {ticketsToBook}\n\nYour Travel date is :{travelDate.ToString("dddd, dd MMMM yyyy")}\n\nThank You For Booking...\n\n");
                                                        _acBuses[busIndex - 1].FilledSeats += ticketsToBook;
                                                        Console.WriteLine($"Available Seats :{_acBuses[busIndex - 1].AvailableSeats}");
                                                        _bookings.Add(new TravelEntity(fromLocation, toLocation + " AC Status : Enabled ", _acBuses[busIndex - 1].BusName, travelDate, ticketsToBook, _acBuses[busIndex - 1].FilledSeats));
                                                        confirmationConfirmed = false;
                                                        acBook = false;
                                                        
                                                    }
                                                    else if (confirmation == "n")
                                                    {
                                                        Console.WriteLine("Your Ticket Cancelled");
                                                        confirmationConfirmed = false;
                                                        acBook = false;
                                                       
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Enter Y or N");
                                                    }
                                                    
                                                }
                                                Console.BackgroundColor = ConsoleColor.Black;
                                            }
                                            else { Console.WriteLine($"Not Enough Available Seats on Choosen Bus {_acBuses[busIndex - 1].BusName}\n"); }
                                        }
                                        else{Console.WriteLine("please choose the available buses..");}
                                        break;
                                    }
                                    else { Console.WriteLine("You can Book for Today and Future Days Only"); }
                                }
                                else { Console.WriteLine("Invalid Date Formate , Enter the Date in the Formate (dd-mm-yyyy)"); }
                            }

                        }
                        else { Console.WriteLine($"For {toLocation} Location Transportaion Not Available"); return; }
                    }
                    else { Console.WriteLine($"For {fromLocation} Location Transportaion Not Available"); return; }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void NonAcBooking()
        {
            try
            {
                bool nonAcBook = true;
                while (nonAcBook)
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
                            if (fromLocation == toLocation) { Console.WriteLine("The destination location must be different from the source location. Please update your entry."); return; }
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine("Enter Date (dd-MM-yyyy) of Travel :");
                                string date = Console.ReadLine();
                                DateTime travelDate;
                                if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out travelDate))
                                {
                                    if (travelDate >= DateTime.Today)
                                    {
                                        for (int b = 0; b < _nonAcBuses.Count; b++)
                                        {
                                            bool isMatchedTravel = false;
                                            foreach (TravelEntity booked in _bookings)
                                            {
                                                string destination = toLocation + " AC Status : Disabled ";
                                                if (booked.Date == travelDate.Date && booked.BusName == _nonAcBuses[b].BusName
                                                    && booked.FromLocation == fromLocation && booked.ToLocation == destination)
                                                {
                                                    isMatchedTravel = true;
                                                    _nonAcBuses[b].FilledSeats = booked.FilledSeats;

                                                }
                                            }
                                            if (!isMatchedTravel) _nonAcBuses[b].FilledSeats = 0;
                                        }

                                        Console.WriteLine("Buses Available For This Location");
                                        for (int buses = 0; buses < _nonAcBuses.Count; buses++)
                                        {
                                            Console.WriteLine($"{buses + 1}. {_nonAcBuses[buses].BusName}\n Available Seats : {_nonAcBuses[buses].AvailableSeats}\n");
                                        }
                                        Console.WriteLine("Select the Bus, You Wish To Journey :\n");
                                        int busIndex = int.Parse(Console.ReadLine());
                                        if (_nonAcBuses[busIndex - 1].AvailableSeats == 0) { Console.WriteLine($"No available seats on {_nonAcBuses[busIndex - 1].BusName} Bus , Choose another bus"); _nonAcBuses.Remove(_nonAcBuses[busIndex - 1]); return; }
                                        if (busIndex > 0 && busIndex <= _nonAcBuses.Count)
                                        {
                                            Console.WriteLine($"How Many Tickets Would You Like To Book (Maximum {_nonAcBuses[busIndex - 1].AvailableSeats} Tickets):\n");
                                            int ticketsToBook = int.Parse(Console.ReadLine());
                                            if (ticketsToBook > 0 && ticketsToBook <= _nonAcBuses[busIndex - 1].AvailableSeats)
                                            {
                                                Console.WriteLine("Confirm the Ticket, Are You Ready For The Journey? (Y/N)\n");
                                                Console.WriteLine();
                                                string confirmation = Console.ReadLine().ToLower();
                                                bool confirmationConfirmed = true;
                                                while (confirmationConfirmed)
                                                {
                                                    if (confirmation == "y")
                                                    {
                                                        Console.BackgroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"Final Ticket\n\nFrom :  + {fromLocation}\n\nTo :  + {toLocation}\n\nAC Status : Enabled\n\nBusName :  + {_nonAcBuses[busIndex - 1].BusName}\n\nBooked Tickets Count : {ticketsToBook}\n\nYour Travel date is :{travelDate.ToString("dddd, dd MMMM yyyy")}\n\nThank You For Booking...\n\n");
                                                        _nonAcBuses[busIndex - 1].FilledSeats += ticketsToBook;
                                                        Console.WriteLine($"Available Seats :{_nonAcBuses[busIndex - 1].AvailableSeats}");
                                                        _bookings.Add(new TravelEntity(fromLocation, toLocation + " AC Status : Disabled ", _nonAcBuses[busIndex - 1].BusName, travelDate, ticketsToBook, _nonAcBuses[busIndex - 1].FilledSeats));
                                                        confirmationConfirmed = false;
                                                        nonAcBook = false;
                                                    }
                                                    else if (confirmation == "n")
                                                    {
                                                        Console.WriteLine("Your Ticket Cancelled\n");
                                                        confirmationConfirmed = false;
                                                        nonAcBook = false;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Enter Y or N");
                                                    }
                                                }
                                                Console.BackgroundColor = ConsoleColor.Black;
                                            }
                                            else { Console.WriteLine($"Not Enough Available Seats on Choosen Bus {_nonAcBuses[busIndex - 1].BusName}\n");}
                                        }
                                        else{ Console.WriteLine("please choose the available buses..\n");}
                                        break;
                                    }
                                    else { Console.WriteLine("You can Book for Today and Future Days Only\n"); }
                                }
                                else { Console.WriteLine("Invalid Date Formate , Enter the Date in the Formate (dd-mm-yyyy)\n"); }

                            }
                        }
                        else { Console.WriteLine($"For {toLocation} Location Transportaion Not Available\n"); return; }
                    }
                    else { Console.WriteLine($"For {fromLocation} Location Transportaion Not Available\n"); return; }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void CancelTicket()
        {
            try
            {

                if (_bookings.Count <= 0) { Console.WriteLine("Bookings List is Empty\n"); return; }
                Console.WriteLine("---------------All Bookings---------------------\n");
                for (int book = 0; book < _bookings.Count; book++)
                {
                    Console.WriteLine($"{book + 1}. Source : {_bookings[book].FromLocation} Destination : {_bookings[book].ToLocation} BusName : {_bookings[book].BusName} Date : {_bookings[book].Date} Ticket Count : {_bookings[book].TicketCount}\n");
                }
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Enter the Booking Number to Cancel :");
                    int bookingIndex = int.Parse(Console.ReadLine());
                    bookingIndex -= 1;
                    if (bookingIndex >= 0 && bookingIndex < _bookings.Count)
                    {
                        Console.WriteLine("Enter the number of tickets you want to cancel :");
                        int ticketsToCancel;
                        if (int.TryParse(Console.ReadLine(), out ticketsToCancel))
                        {
                            if (ticketsToCancel > 0 && ticketsToCancel <= _bookings[bookingIndex].TicketCount)
                            {

                                _bookings[bookingIndex].TicketCount -= ticketsToCancel;
                                _bookings[bookingIndex].FilledSeats -= ticketsToCancel;
                                Console.WriteLine($"{ticketsToCancel} ticket will be cancelled.\n");
                                if (_bookings[bookingIndex].TicketCount <= 0) { _bookings.Remove(_bookings[bookingIndex]); }
                                return;
                            }
                            else { Console.WriteLine("Invalid Number of Tickets\n"); }
                        }
                        else { Console.WriteLine("Invalid Input Please Try Again \n"); }
                    }
                    else { Console.WriteLine("Invalid Booking Number \n"); }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
           
        public void CheckBookingStatus()
        {
            try
            {
                if (_bookings.Count <= 0) { Console.WriteLine("No Bookings Found .\n"); return; }
                Console.WriteLine("---------------Your Bookings---------------------\n");
                for (int book = 0; book < _bookings.Count; book++)
                {
                    Console.WriteLine($"{book + 1}. Source : {_bookings[book].FromLocation} Destination : {_bookings[book].ToLocation} BusName : {_bookings[book].BusName} Date : {_bookings[book].Date} Ticket Count : {_bookings[book].TicketCount}\n");
                }
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Please Enter the booking Id to Search :");
                    int bookingId;
                    if (int.TryParse(Console.ReadLine(), out bookingId))
                    {
                        if (bookingId > 0 && bookingId <= _bookings.Count)
                        {
                            for (int book = 0; book < _bookings.Count; book++)
                            {
                                if (bookingId == book + 1)
                                {
                                    Console.WriteLine($"{book + 1}. Source : {_bookings[book].FromLocation} Destination : {_bookings[book].ToLocation} BusName : {_bookings[book].BusName} Date : {_bookings[book].Date} Ticket Count : {_bookings[book].TicketCount}\n");
                                    return;
                                }
                            }
                        }
                        else { Console.WriteLine("Invalid Booking Id,Please Try Again..."); }
                    }
                    else { Console.WriteLine("Invalid Input Please Try Again \n"); }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
    
}
