using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.Services;
using RepositoryLayer.Models;
using System.Collections.ObjectModel;

namespace PresentationLayer.ViewModels
{
    public class ListBookingReservationHistoryViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private readonly BookingReservationDetailViewModel _bookingReservationDetailViewModel;
        private ObservableCollection<BookingReservationItemViewModel> _bookingReservations;
        public ObservableCollection<BookingReservationItemViewModel> BookingReservations
        {
            get => _bookingReservations;
            set
            {
                _bookingReservations = value;
                OnPropertyChanged(nameof(BookingReservations));
            }
        }

        public ListBookingReservationHistoryViewModel(IBookingReservationService bookingReservationService, 
            BookingReservationDetailViewModel bookingReservationDetailViewModel)
        {
            _bookingReservationService = bookingReservationService;
            _bookingReservationDetailViewModel = bookingReservationDetailViewModel;
        }

        public async Task GetBookingReservationsAsync(int customerId)
        {
            var bookingReservations = await _bookingReservationService.GetBookingReservationsByCustomerIdAsync(customerId);

            var bookingObservable = new ObservableCollection<BookingReservationItemViewModel>();
            foreach (var booking in bookingReservations)
            {
                var bookingDetail = new BookingReservationItemViewModel(_bookingReservationDetailViewModel);
                bookingDetail.BookingReservationID = booking.BookingReservationID;
                bookingDetail.BookingDate = booking.BookingDate;
                bookingDetail.BookingStatus = booking.BookingStatus;
                bookingDetail.TotalPrice = booking.TotalPrice;
                bookingObservable.Add(bookingDetail);
            }

            BookingReservations = bookingObservable;
        }


    }
}
