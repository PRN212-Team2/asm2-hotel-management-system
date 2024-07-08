using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
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
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IRoomInformationRepository _roomInformationRepository;
        private readonly IMapper _mapper;

        public BookingReservationService(IBookingReservationRepository bookingReservationRepository,
            IBookingDetailRepository bookingDetailRepository, IRoomInformationRepository roomInformationRepository,
            IMapper mapper)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _roomInformationRepository = roomInformationRepository;
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

        public async Task MakeReservation(int customerId, IReadOnlyList<BasketItemDTO> basketItems)
        {
            decimal totalPrice = 0;
            int revId = await _bookingReservationRepository.GenerateNewIdAsync();
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            
            foreach(var item in basketItems)
            {
                var room = await _roomInformationRepository.GetRoomByIdWithTypeAsync(item.RoomID);
                int numberOfDays = (item.EndDate - item.StartDate).Days + 1;
                decimal roomPrice = room.RoomPricePerDay * numberOfDays;
                totalPrice += roomPrice;
                var bookingDetail = new BookingDetail(revId, item.RoomID, item.StartDate, item.EndDate, room.RoomPricePerDay);
                bookingDetails.Add(bookingDetail);
            }

            var reservation = new BookingReservation(revId, totalPrice, customerId, true);

            await _bookingReservationRepository.AddBookingReservationAsync(reservation);

            foreach (var bookingDetail in bookingDetails)
            {
                await _bookingDetailRepository.AddBookingDetailAsync(bookingDetail);
                
            }
        }
    }
}
