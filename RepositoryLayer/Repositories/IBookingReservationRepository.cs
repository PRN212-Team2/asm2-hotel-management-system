using RepositoryLayer.Models;

namespace RepositoryLayer.Repositories
{
    public interface IBookingReservationRepository
    {
        Task<IReadOnlyList<BookingReservation>> GetBookingReservationsByCustomerIdAsync(int customerId);
        Task<IReadOnlyList<BookingReservation>> GetBookingReservationsAsync();
        Task<IReadOnlyList<BookingReservation>> GetFilteredBookingReservationsAsync(DateTime StartDate, DateTime EndDate);
        Task<BookingReservation> GetBookingReservationByIdAsync(int id);
    }
}
