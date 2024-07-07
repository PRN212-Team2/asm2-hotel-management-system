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

        public async Task<BookingReservationDetailDTO> GetBookingReservationDetailByIdAsync(int id)
        {
            var bookingReservation = await _bookingReservationRepository.GetBookingReservationByIdAsync(id);
            return _mapper.Map<BookingReservation, BookingReservationDetailDTO>(bookingReservation);
        }

        public async Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsByCustomerIdAsync(int customerId)
        {
            var bookingReservations = await _bookingReservationRepository.GetBookingReservationsByCustomerIdAsync(customerId);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationDTO>>(bookingReservations);
        }

        
    }
}
