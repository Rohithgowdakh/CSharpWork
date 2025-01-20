using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public class FlightModel
    {
        public string FlightName { get; set; }
        public int TotalSeats { get; set; }
        public int FilledSeats { get; set; }
        public int AvailableSeats => TotalSeats - FilledSeats;
        public FlightModel(string busName, int totalSeats, int filledSeats)
        {
            FlightName = busName;
            TotalSeats = totalSeats;
            FilledSeats = filledSeats;
        }
    }
}
