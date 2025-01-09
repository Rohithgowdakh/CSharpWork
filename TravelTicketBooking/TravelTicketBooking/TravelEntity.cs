using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public  class TravelEntity
    {
        public string FromLocation {  get; set; }
        public string ToLocation { get; set; }

        public string BusName {  get; set; }
        public DateTime Date { get; set; }

        public int TicketCount {  get; set; }

        public int FilledSeats {  get; set; }
        public TravelEntity( string fromLocation,string toLocation,string busName,DateTime date,int ticketCount,int filledSeats)
        {
           
            this.FromLocation = fromLocation;
            this.ToLocation = toLocation;
            this.BusName = busName;
            this.Date = date;
            this.TicketCount = ticketCount;
            this.FilledSeats = filledSeats;
        }
    }
}
