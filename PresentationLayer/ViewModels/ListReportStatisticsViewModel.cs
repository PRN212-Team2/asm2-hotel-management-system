using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
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
        private decimal _revenue;
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

        public decimal Revenue
        {
            get => _revenue;
            set
            {
                _revenue = value;
                OnPropertyChanged(nameof(Revenue));
            }
        }

        public RelayCommand FilterCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }

        public ListReportStatisticsViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
            FilterCommand = new RelayCommand(async o => await FilterBookingReservationsAsync(o), o => true);
            ResetFilterCommand = new RelayCommand(async o => await ResetFilter(o), o => true);
            // Initialize StartDate and EndDate if needed
        }

        public async Task GetBookingReservationsAsync()
        {
            var bookingReservations = await _bookingReservationService.GetBookingReservationsAsync();
            ReportStatistics = new ObservableCollection<BookingReservationReportStatisticDTO>(bookingReservations);
            CalculateRevenue();
        }

        private async Task FilterBookingReservationsAsync(object obj)
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
            CalculateRevenue();
        }

        private async Task ResetFilter(object obj)
        {
            await GetBookingReservationsAsync();
            StartDate = null;
            EndDate = null;
        }

        private void CalculateRevenue()
        {
            if(ReportStatistics != null)
            {
                Revenue = ReportStatistics.Sum(r => r.TotalPrice);
            }
            else Revenue = 0;
        }
    }
}