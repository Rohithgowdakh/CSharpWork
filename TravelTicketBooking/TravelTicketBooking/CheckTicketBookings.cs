using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public class CheckTicketBookings
    {
        TicketBookingClass CheckingTicket;

        public CheckTicketBookings(TicketBookingClass CheckingTicket)
        {
            this.CheckingTicket = CheckingTicket;
        }
        
        /// <summary>
        /// Displays all bookings and allows the user to search for a specific booking by ID, showing its details if found.
        /// </summary>
        public void CheckBookingStatus()
        {
            try
            {
                List<TravelModel> bookings = CheckingTicket._bookings;
                if (bookings == null || bookings.Count <= 0) { Console.WriteLine("No Bookings Found .\n"); return; }
                Console.WriteLine("---------------Your Bookings---------------------\n");
                for (int book = 0; book < bookings.Count; book++)
                {
                    Console.WriteLine($"{book + 1}. Source : {bookings[book].FromLocation} Destination : {bookings[book].ToLocation} Ticket Class : {bookings[book].TicketClass} Date : {bookings[book].Date}\n");
                }
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Please enter the booking ID to search:");
                    int bookingId;
                    if (int.TryParse(Console.ReadLine(), out bookingId))
                    {
                        if (bookingId > 0 && bookingId <= bookings.Count)
                        {
                            for (int book = 0; book < bookings.Count; book++)
                            {
                                if (bookingId == book + 1)
                                {
                                    Console.WriteLine($"\n{book + 1}. Source : {bookings[book].FromLocation} Destination : {bookings[book].ToLocation} BusName : {bookings[book].FlightName} Date : {bookings[book].Date}\nTicket Count : {bookings[book].TicketCount}" +
                                        $" Ticket Class : {bookings[book].TicketClass}\n\n");
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
