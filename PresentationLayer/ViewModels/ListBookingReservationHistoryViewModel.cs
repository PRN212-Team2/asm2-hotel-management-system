using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using System.Collections.ObjectModel;

namespace PresentationLayer.ViewModels
{
    public class ListBookingReservationHistoryViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private ObservableCollection<BookingReservationDTO> _bookingReservations;
        public ObservableCollection<BookingReservationDTO> BookingReservations
        {
            get => _bookingReservations;
            set
            {
                _bookingReservations = value;
                OnPropertyChanged(nameof(BookingReservations));
            }
        }

        public ListBookingReservationHistoryViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
        }

        public async Task GetBookingReservationsAsync(int customerId)
        {
            var bookingReservations = await _bookingReservationService.GetBookingReservationsByCustomerIdAsync(customerId);
            BookingReservations = new ObservableCollection<BookingReservationDTO>(bookingReservations);
        }


    }
}
