using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public  class TravelModel
    {
        public string FromLocation {  get; set; }
        public string ToLocation { get; set; }
        public string FlightName {  get; set; }
        public string TicketClass {  get; set; }
        public DateTime Date { get; set; }
        public int TicketCount {  get; set; }
        public int FilledSeats {  get; set; }
        //Pass Object to the constuctor
        public TravelModel(TravelModel travelEntity)
        {

            this.FromLocation = travelEntity.FromLocation;
            this.ToLocation   = travelEntity.ToLocation  ;
            this.FlightName = travelEntity.FlightName;
            this.Date = travelEntity.Date;
            this.TicketCount = travelEntity.TicketCount;
            this.FilledSeats  = travelEntity.FilledSeats;
            this.TicketClass = travelEntity.TicketClass;
        }
        public TravelModel() { }
    }
}
