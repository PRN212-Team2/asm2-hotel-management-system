using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using PresentationLayer.Services;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class BookingReservationItemViewModel : ViewModelBase
    {
        private readonly BookingReservationDetailViewModel _bookingReservationDetailViewModel;

        public int BookingReservationID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool BookingStatus { get; set; }
        public RelayCommand ShowBookingDetailViewCommand { get; set; }

        public BookingReservationItemViewModel(BookingReservationDetailViewModel bookingReservationDetailViewModel) 
        {
            _bookingReservationDetailViewModel = bookingReservationDetailViewModel;
            ShowBookingDetailViewCommand = new RelayCommand(async o => await ShowBookingDetailWindow(o), o => true);
        }

        private async Task ShowBookingDetailWindow(object obj)
        {
            await _bookingReservationDetailViewModel.GetBookingReservationDetails((int)obj);
            var bookingDetailWindow = new BookingReservationDetailView(_bookingReservationDetailViewModel);
            bookingDetailWindow.Show();
        }
    }
}
