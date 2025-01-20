namespace TravelTicketBooking
{
    public class TicketCancelling
    {
        private TicketBookingClass ticketBooking;
        public TicketCancelling(TicketBookingClass ticketBook)
        {
            ticketBooking=ticketBook;
        }

        /// <summary>
        /// Displays all bookings, allows the user to select a booking to cancel, and updates the ticket count and filled seats accordingly.
        /// Removes the booking if all tickets are canceled.
        /// </summary>
        public void CancelBooking()
        {
            try
            {
                var bookings = ticketBooking._bookings;
                if (bookings == null || bookings.Count <= 0)
                {
                    Console.WriteLine("Bookings List is Empty\n");
                    return;
                }

                Console.WriteLine("---------------All Bookings---------------------\n");
                for (int book = 0; book < bookings.Count; book++)
                {
                    Console.WriteLine($"{book + 1}. Source : {bookings[book].FromLocation} Destination : {bookings[book].ToLocation} FlightName : {bookings[book].FlightName} Date : {bookings[book].Date}\nTicket Count : {bookings[book].TicketCount}" +
                                      $" Ticket Class : {bookings[book].TicketClass}\n");
                }

                for (int i = 0; i < 3; i++) // Allow 3 attempts to enter a valid booking number
                {
                    Console.WriteLine("Enter the booking number to cancel:");
                    int bookingIndex = int.Parse(Console.ReadLine());
                    bookingIndex -= 1;

                    if (bookingIndex >= 0 && bookingIndex < bookings.Count)
                    {
                        Console.WriteLine("Enter the number of tickets you wish to cancel:");
                        int ticketsToCancel;
                        if (int.TryParse(Console.ReadLine(), out ticketsToCancel))
                        {
                            if (ticketsToCancel > 0 && ticketsToCancel <= bookings[bookingIndex].TicketCount)
                            {
                                bookings[bookingIndex].TicketCount -= ticketsToCancel;
                                foreach (var booking in bookings)
                                {
                                    if (booking.FlightName == bookings[bookingIndex].FlightName &&
                                        booking.Date == bookings[bookingIndex].Date &&
                                        booking.FromLocation == bookings[bookingIndex].FromLocation &&
                                        booking.ToLocation == bookings[bookingIndex].ToLocation)
                                    {
                                        booking.FilledSeats -= ticketsToCancel;
                                    }
                                }

                                Console.WriteLine($"\n{ticketsToCancel} ticket(s) will be canceled.\n");
                                if (bookings[bookingIndex].TicketCount <= 0)
                                {
                                    bookings.RemoveAt(bookingIndex);
                                }
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of tickets.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please try again.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid booking number.\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

    }
}
