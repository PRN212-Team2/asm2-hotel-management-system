using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Services
{
    public interface IBookingReservationService
    {
        Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsByCustomerIdAsync(int customerId);
        Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetBookingReservationsAsync();
        Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetFilteredBookingReservationsAsync(DateTime StartDate, DateTime EndDate);
    }
}
