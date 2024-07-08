using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for BookingReservationDetail.xaml
    /// </summary>
    public partial class BookingReservationDetailView : Window
    {
        private readonly BookingReservationDetailViewModel _bookingReservationDetailViewModel;

        public BookingReservationDetailView(BookingReservationDetailViewModel bookingReservationDetailViewModel)
        {
            InitializeComponent();
            _bookingReservationDetailViewModel = bookingReservationDetailViewModel;
            this.DataContext = _bookingReservationDetailViewModel;
        }
    }
}
