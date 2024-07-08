using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Models
{
    public class BookingReservation
    {
        public int BookingReservationID { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public int CustomerID { get; set; }
        public bool BookingStatus { get; set; }
        public IReadOnlyList<BookingDetail> BookingDetails { get; set; }

        public BookingReservation() { }

        public BookingReservation(int bookingReservationID, decimal totalPrice, int customerID, bool bookingStatus)
        {
            BookingReservationID = bookingReservationID;
            BookingDate = DateTime.Now;
            TotalPrice = totalPrice;
            CustomerID = customerID;
            BookingStatus = bookingStatus;
        }
    }
}
