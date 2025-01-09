using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public class BusEntity
    {
        public string BusName { get; set; }

        public int TotalSeats { get; set; }

        public int FilledSeats { get; set; }
        public BusEntity(string busName, int totalSeats, int filledSeats)
        {
            BusName = busName;
            TotalSeats = totalSeats;
            FilledSeats = filledSeats;
        }

        public int AvailableSeats => TotalSeats - FilledSeats;

       
        public BusEntity() { }
    }
}
