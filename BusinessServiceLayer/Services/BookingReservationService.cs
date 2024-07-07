using AutoMapper;
using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IMapper _mapper;

        public BookingReservationService(IBookingReservationRepository bookingReservationRepository, IMapper mapper)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsByCustomerIdAsync(int customerId)
        {
            var bookingReservations = await _bookingReservationRepository.GetBookingReservationsByCustomerIdAsync(customerId);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationDTO>>(bookingReservations);
        }

        public async Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetBookingReservationsAsync()
        {
            var bookingReservations = await _bookingReservationRepository.GetBookingReservationsAsync();
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationReportStatisticDTO>>(bookingReservations);
        }

        public async Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetFilteredBookingReservationsAsync(DateTime StartDate, DateTime EndDate)
        {
            var bookingReservations = await _bookingReservationRepository.GetFilteredBookingReservationsAsync(StartDate, EndDate);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationReportStatisticDTO>>(bookingReservations);
        }
    }
}
