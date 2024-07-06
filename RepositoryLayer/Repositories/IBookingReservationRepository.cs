﻿using RepositoryLayer.Models;

namespace RepositoryLayer.Repositories
{
    public interface IBookingReservationRepository
    {
        Task<IReadOnlyList<BookingReservation>> GetBookingReservationsByCustomerIdAsync(int customerId);
    }
}
