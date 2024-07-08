using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class MakeReservationRoomItemViewModel : ViewModelBase
    {
        private readonly RoomInformationDetailsViewModel _roomInformationDetailsViewModel;

        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }

        public RelayCommand ShowRoomInformationDetailsViewCommand { get; set; }

        public MakeReservationRoomItemViewModel(RoomInformationDetailsViewModel roomInformationDetailsViewModel)
        {
            _roomInformationDetailsViewModel = roomInformationDetailsViewModel;
            ShowRoomInformationDetailsViewCommand = new RelayCommand(async o => await ShowRoomInformationDetailsView(o),
                o => true);
        }

        private async Task ShowRoomInformationDetailsView(object obj)
        {
            await _roomInformationDetailsViewModel.LoadRoomDetails((int)obj);
            var roomDetailsWin = new RoomInformationDetailsView(_roomInformationDetailsViewModel);
            roomDetailsWin.Show();
        }
    }
}
