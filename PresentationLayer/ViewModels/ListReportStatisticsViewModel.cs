using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModels
{
    public class ListReportStatisticsViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private ObservableCollection<BookingReservationReportStatisticDTO> _reportStatistics;
        public ObservableCollection<BookingReservationReportStatisticDTO> ReportStatistics
        {
            get => _reportStatistics;
            set
            {
                _reportStatistics = value;
                OnPropertyChanged(nameof(ReportStatistics));
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand FilterCommand { get; }

        public ListReportStatisticsViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
            FilterCommand = new RelayCommand(async o => await FilterBookingReservationsAsync(), CanFilterExecute);
            // Initialize StartDate and EndDate if needed
        }

        public async Task GetBookingReservationsAsync()
        {
            var bookingReservations = await _bookingReservationService.GetBookingReservationsAsync();
            ReportStatistics = new ObservableCollection<BookingReservationReportStatisticDTO>(bookingReservations);
        }

        private async Task FilterBookingReservationsAsync()
        {
            if (!StartDate.HasValue || !EndDate.HasValue)
            {
                MessageBox.Show("Please provide both start and end dates.", "Date Missing", MessageBoxButton.OK);
                return;
            }

            if (StartDate > EndDate)
            {
                MessageBox.Show("Start date cannot be greater than end date.", "Invalid Date Range", MessageBoxButton.OK);
                return;
            }

            var bookingReservations = await _bookingReservationService.GetFilteredBookingReservationsAsync(StartDate.Value, EndDate.Value);
            ReportStatistics = new ObservableCollection<BookingReservationReportStatisticDTO>(bookingReservations);
        }

        private bool CanFilterExecute(object parameter)
        {
            return true;
        }
    }
}