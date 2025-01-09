using System;
namespace TravelTicketBooking
{
    public class Travel
    {
        public static void Main(string[] args)
        {
            TravelUtility book = new TravelUtility();
            book.InitializeListItems();
            book.LoginPage();
        }
    }
}
