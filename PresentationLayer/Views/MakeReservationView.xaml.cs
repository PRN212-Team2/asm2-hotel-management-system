using PresentationLayer.Models;
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
    /// Interaction logic for MakeReservationView.xaml
    /// </summary>
    public partial class MakeReservationView : UserControl
    {
        public MakeReservationView()
        {
            InitializeComponent();
        }

        private void Remove_Item(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                BasketItem basketItem = (BasketItem) element.DataContext;
                BasketManager.DeleteBasketItem(basketItem.RoomID);                                     
            }
        }
    }
}
