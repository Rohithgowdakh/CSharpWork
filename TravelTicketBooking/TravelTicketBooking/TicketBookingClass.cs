namespace TravelTicketBooking
{
    public class TicketBookingClass
    {
        public List<FlightEntity> _businessClass = new List<FlightEntity>();
        public List<FlightEntity> _economyClass = new List<FlightEntity>();
        public List<string> _location = new List<string>();
        public List<TravelEntity> _bookings { get; set; } = new List<TravelEntity>();
        /// <summary>
        /// Initializes the flight and location lists.
        /// </summary>
        public void InitializeListItems()
        {
            InitBusinessClassFlights();
            InitEconomyClassFlights();
            InitLocation();
        }
        /// <summary>
        /// Adds a list of locations to the _location list.
        /// </summary>
        private void InitLocation()
        {
            _location.Add("new york");
            _location.Add("los angeles");
            _location.Add("london");
            _location.Add("paris");
            _location.Add("berlin");
            _location.Add("tokyo");
            _location.Add("sydney");
            _location.Add("dubai");
            _location.Add("rome");
            _location.Add("moscow");
            _location.Add("toronto");
            _location.Add("seoul");
            _location.Add("singapore");
            _location.Add("madrid");
            _location.Add("toronto");
            _location.Add("mumbai");
            _location.Add("hong kong");
            _location.Add("bangkok");
            _location.Add("dubai");
            _location.Add("rio de janeiro");
            _location.Add("cape town");
            _location.Add("toronto");
            _location.Add("cairo");
            _location.Add("beijing");
            _location.Add("seoul");
            _location.Add("mexico city");
            _location.Add("san francisco");
            _location.Add("amsterdam");
            _location.Add("lisbon");
            _location.Add("lagos");
            _location.Add("bangalore");
        }
        /// <summary>
        /// Initializes the list of Economy Class flights with flight names, seat capacity, and filled seats.
        /// </summary>
        private void InitEconomyClassFlights()
        {
            try
            {
                _economyClass.Add(new("new york airlines", 200, 0));
                _economyClass.Add(new("los angeles flights", 150, 0));
                _economyClass.Add(new("london express", 180, 0));
                _economyClass.Add(new("paris charters", 160, 0));

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Initializes the list of Business Class flights with flight names, seat capacity, and filled seats.
        /// </summary>
        private void InitBusinessClassFlights()
        {
            try
            {
                _businessClass.Add(new("american airlines", 20, 0));
                _businessClass.Add(new("emirates", 30, 0));
                _businessClass.Add(new("british airways", 25, 0));
                _businessClass.Add(new("lufthansa", 35, 0));
                _businessClass.Add(new("qatar airways", 40, 0));

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Allows the user to choose Business or Economy Class for flight booking, 
        /// checks if the input is valid, and returns the corresponding choice or an error message.
        /// </summary>
        public int SelectFlightClass()
        {
            try
            {
                Console.WriteLine("Select Available Flight Facility:");
                Console.WriteLine("1. Book Business Class");
                Console.WriteLine("2. Book Economy Class");
                Console.WriteLine();
                Console.WriteLine("Enter Your Choice :");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    if (choice == 1)
                    {
                        return 1;
                    }
                    else if (choice == 2)
                    {
                        return 2;
                    }
                    else { Console.WriteLine("Enter a Valid Choice..\n"); return -1; }
                }
                else { Console.WriteLine("Invalid Input Please try again\n"); return -1; }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        /// <summary>
        /// Handles the ticket booking process based on the user's choice of Business or Economy Class. 
        /// Validates locations, travel date, and displays available flights. Finalizes the ticket booking 
        /// if valid flights are available.
        /// </summary>
        public void BookTicket(int ticketChoice)
        {
            try
            {
                bool businessClassTicketBooking = true;
                List<FlightEntity> selectedFlight = GetFlightListByClass(ticketChoice);
                if (selectedFlight == null) return;
                while (businessClassTicketBooking)
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
                            if (fromLocation == toLocation)
                            {
                                Console.WriteLine("The destination location must be different from the source location. Please update your entry.");
                                return;
                            }
                            DateTime travelDate = GetTravelDate();
                            if (travelDate == default) return;
                            UpdateFlightAvailability(selectedFlight, fromLocation, toLocation + $"{(ticketChoice == 1 ? "Air Conditioning: Available " : "Air Conditioning: Disabled ")}", travelDate);
                            Console.WriteLine("Flights Available for This Location\n");
                            for (int flight = 0; flight < selectedFlight.Count; flight++)
                            {
                                Console.WriteLine($"{flight + 1}. {selectedFlight[flight].FlightName}\n Available Seats : {selectedFlight[flight].AvailableSeats}\n");
                            }
                            FinalFlightTicket(selectedFlight, fromLocation, toLocation,ticketChoice,travelDate,ref businessClassTicketBooking);
                        }
                        else { Console.WriteLine($"Transportation to {toLocation} is not available."); return; }
                    }
                    else { Console.WriteLine($"Transportation to {fromLocation} is not available."); return; }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Retrieves the list of flights based on the user's ticket choice (1 for Business Class, 2 for Economy Class). 
        /// Returns null if the choice is invalid.
        /// </summary>
        private List<FlightEntity> GetFlightListByClass(int ticketChoice)
        {
            List<FlightEntity> selectedFlight;
            switch (ticketChoice)
            {
                case 1: return selectedFlight = _businessClass;
                case 2: return selectedFlight = _economyClass;
                default:
                    Console.WriteLine("Invalid choice! Please select 1 for Business Class or 2 for Economy Class.");
                    return selectedFlight = null;
            }
        }
        /// <summary>
        /// Prompts the user to enter a travel date in the format (dd-mm-yyyy), validates the input, 
        /// and returns the date if valid and in the present or future. Returns default if the input is invalid after 3 attempts.
        /// </summary>
        private DateTime GetTravelDate()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Enter Date (dd-mm-yyyy) of Travel :");
                string date = Console.ReadLine();
                DateTime travelDate;
                if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out travelDate))
                {
                    if (travelDate >= DateTime.Today)
                    {
                        return travelDate;
                    }
                    else { Console.WriteLine("You can Book for Today and Future Days Only"); }

                }
                else { Console.WriteLine("Invalid Date Formate , Enter the Date in the Formate (dd-mm-yyyy)"); }
            }
            return default;
        }
        /// <summary>
        /// Updates the availability of flights by checking bookings for the specified travel date, 
        /// source, and destination locations. If no matching booking is found, sets filled seats to zero.
        /// </summary>
        private void UpdateFlightAvailability(List<FlightEntity> flights, string fromLocation, string toLocation, DateTime travelDate)
        {
            try
            {
                foreach (var flight in flights)
                {
                    bool isMatched = false;
                    foreach (var booking in _bookings)
                    {
                        if (booking.Date == travelDate && booking.FlightName == flight.FlightName &&
                            booking.FromLocation == fromLocation && booking.ToLocation == toLocation)
                        {
                            isMatched = true;
                            flight.FilledSeats = booking.FilledSeats;
                        }
                    }
                    if (!isMatched) flight.FilledSeats = 0;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Finalizes the ticket booking process by allowing the user to select a flight, confirm the number of tickets, 
        /// and validate booking details. Updates flight availability and booking records or cancels the process based on user input.
        /// </summary>
        private void FinalFlightTicket(List<FlightEntity> selectedFlight, string fromLocation, string toLocation, int ticketChoice, DateTime travelDate,ref bool businessClassTicketBooking )
        {
            try
            {
                Console.WriteLine("Select the Flight You Wish to Travel On:\n");
                int flightIndex = int.Parse(Console.ReadLine());
                if (selectedFlight[flightIndex - 1].AvailableSeats == 0) { Console.WriteLine($"No available seats on {selectedFlight[flightIndex - 1].FlightName} flight. Please choose another flight."); selectedFlight.Remove(selectedFlight[flightIndex - 1]); return; }
                if (flightIndex > 0 && flightIndex <= selectedFlight.Count)
                {
                    Console.WriteLine($"How Many Tickets Would You Like To Book (Maximum {selectedFlight[flightIndex - 1].AvailableSeats} Tickets):\n");
                    int ticketsToBook = int.Parse(Console.ReadLine());
                    if (ticketsToBook > 0 && ticketsToBook <= selectedFlight[flightIndex - 1].AvailableSeats)
                    {
                        bool confirmationConfirmed = true;
                        while (confirmationConfirmed)
                        {
                            Console.WriteLine("Confirm your booking. Are you ready for the journey? (Y/N)");
                            Console.WriteLine();
                            string confirmation = Console.ReadLine().ToLower();
                            if (confirmation == "y")
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"Flight Ticket\n\nFrom :   {fromLocation}\n\nTo :   {toLocation}\n\n{(ticketChoice == 1 ? " Air Conditioning: Available " : " Air Conditioning: Disabled ")}\n\nFlight Name :   {selectedFlight[flightIndex - 1].FlightName}\n\n" +
                                                    $"Class : {(ticketChoice == 1 ? "Business Class" : "Economy Class")}\n\nBooked Tickets Count : {ticketsToBook}\n\nYour Travel Date Is : {travelDate.ToString("dddd, dd MMMM yyyy")}\n\nThank You For Booking...\n\n");
                                selectedFlight[flightIndex - 1].FilledSeats += ticketsToBook;
                                Console.WriteLine($"Available Seats :{selectedFlight[flightIndex - 1].AvailableSeats}");
                                TravelEntity travelEntity = new TravelEntity();
                                travelEntity.FromLocation = fromLocation;
                                travelEntity.ToLocation = toLocation + $"{(ticketChoice == 1 ? " Air Conditioning: Available " : " Air Conditioning: Disabled ")}";
                                travelEntity.FlightName = selectedFlight[flightIndex - 1].FlightName;
                                travelEntity.Date = travelDate;
                                travelEntity.TicketCount = ticketsToBook;
                                travelEntity.FilledSeats = selectedFlight[flightIndex - 1].FilledSeats;
                                travelEntity.TicketClass = ticketChoice == 1 ? "Business Class" : "Economy Class";
                                _bookings.Add(new TravelEntity(travelEntity));
                                confirmationConfirmed = false;
                                businessClassTicketBooking = false;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;

                            }
                            else if (confirmation == "n")
                            {
                                Console.WriteLine("Your Ticket Cancelled\n");
                                confirmationConfirmed = false;
                                businessClassTicketBooking = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter Y or N");
                            }
                        }
                    }
                    else { Console.WriteLine($"Not Enough Available Seats on Choosen Flight {selectedFlight[flightIndex - 1].FlightName}\n"); }
                }
                else { Console.WriteLine("Please choose an available flight."); }
            }
            catch ( Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
