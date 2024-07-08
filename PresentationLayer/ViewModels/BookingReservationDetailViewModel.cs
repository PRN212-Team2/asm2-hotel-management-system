using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class BookingReservationDetailViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;

        public BookingReservationDetailDTO BookingReservation { get; set; }

        public BookingReservationDetailViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
        }

        public async Task GetBookingReservationDetails(int id)
        {
            var bookingReservation = await _bookingReservationService.GetBookingReservationDetailByIdAsync(id);
            BookingReservation = bookingReservation;
        }
    }
}
