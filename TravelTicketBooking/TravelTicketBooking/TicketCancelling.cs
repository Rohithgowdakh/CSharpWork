namespace TravelTicketBooking
{
    public class TicketCancelling
    {
        private TicketBookingClass ticketCancelObj;
        public TicketCancelling(TicketBookingClass ticketBook)
        {
            ticketCancelObj=ticketBook;
        }
        /// <summary>
        /// Displays all bookings, allows the user to select a booking to cancel, and updates the ticket count and filled seats accordingly.
        /// Removes the booking if all tickets are canceled.
        /// </summary>
        public void CancelBooking()
        {
            try
            {

                var bookings =ticketCancelObj._bookings;
                if (bookings == null || bookings.Count <= 0) { Console.WriteLine("Bookings List is Empty\n"); return; }
                Console.WriteLine("---------------All Bookings---------------------\n");
                for (int book = 0; book < bookings.Count; book++)
                {
                    Console.WriteLine($"{book + 1}. Source : {bookings[book].FromLocation} Destination : {bookings[book].ToLocation} BusName : {bookings[book].FlightName} Date : {bookings[book].Date}\nTicket Count : {bookings[book].TicketCount}" +
                        $" Ticket Class : {bookings[book].TicketClass}");
                }
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Enter the booking number to cancel:");
                    int bookingIndex = int.Parse(Console.ReadLine());
                    bookingIndex -= 1;
                    if (bookingIndex >= 0 && bookingIndex < bookings.Count)
                    {
                        Console.WriteLine("Enter the number of tickets you wish to cancel :");
                        int ticketsToCancel;
                        if (int.TryParse(Console.ReadLine(), out ticketsToCancel))
                        {
                            if (ticketsToCancel > 0 && ticketsToCancel <= bookings[bookingIndex].TicketCount)
                            {

                                bookings[bookingIndex].TicketCount -= ticketsToCancel;
                                bookings[bookingIndex].FilledSeats -= ticketsToCancel;
                                Console.WriteLine($"\n{ticketsToCancel} ticket will be cancelled.\n");
                                if (bookings[bookingIndex].TicketCount <= 0) { bookings.Remove(bookings[bookingIndex]); }
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
    }
}
