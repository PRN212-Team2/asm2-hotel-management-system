using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class RoomInformationDetailsViewModel
    {
        private readonly UpdateRoomInformationViewModel _updateRoomInformationViewModel;
        private readonly DeleteRoomInformationViewModel _deleteRoomInformationViewModel;

        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public bool RoomStatus { get; set; }

        public decimal RoomPricePerDay { get; set; }

        public RelayCommand ShowUpdateRoomInformationWindow { get; set; }
        public RelayCommand ShowDeleteRoomInformationWindow { get; set; }


        public UpdateRoomInformationPopupView updateRoomInformationWin;

        public DeleteRoomInformationPopupView deleteRoomInformationWin;

        public RoomInformationDetailsViewModel(UpdateRoomInformationViewModel updateRoomInformationViewModel,
            DeleteRoomInformationViewModel deleteRoomInformationViewModel)
        {
            _updateRoomInformationViewModel = updateRoomInformationViewModel;
            _deleteRoomInformationViewModel = deleteRoomInformationViewModel;
            ShowUpdateRoomInformationWindow = new RelayCommand(async o => await ShowUpdateWindow(o), o => true);
            ShowDeleteRoomInformationWindow = new RelayCommand(ShowDeleteWindow, o => true);
        }

        private async Task ShowUpdateWindow(object roomId)
        {
            if (roomId != null)
            {
                await _updateRoomInformationViewModel.LoadRoomInformationDetail((int)roomId);
                updateRoomInformationWin = new UpdateRoomInformationPopupView(_updateRoomInformationViewModel);
                updateRoomInformationWin.Show();
            }
            else
            {
                MessageBox.Show("Room ID not found");
            }
        }

        private void ShowDeleteWindow(object roomId)
        {
            if (roomId != null)
            {
                _deleteRoomInformationViewModel.RoomID = (int)roomId;
                deleteRoomInformationWin = new DeleteRoomInformationPopupView(_deleteRoomInformationViewModel);
                deleteRoomInformationWin.Show();
            }
            else
            {
                MessageBox.Show("Room ID not found");
            }
        }
    }
}
