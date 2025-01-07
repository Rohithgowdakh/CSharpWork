using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTicketBooking
{
    public  class TravelEntity
    {
        public int BookId { get; set; }
        public string FromLocation {  get; set; }
        public string ToLocation { get; set; }
        public DateOnly Date { get; set; }

        public TravelEntity(int  bookId, string fromLocation,string toLocation,DateOnly date)
        {
            this.BookId = bookId;
            this.FromLocation = fromLocation;
            this.ToLocation = toLocation;
            this.Date = date;
        }
    }
}
