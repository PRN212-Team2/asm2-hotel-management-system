using BusinessServiceLayer.Interfaces;
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
    public class RoomInformationItemViewModel : ViewModelBase
    {
        private readonly UpdateRoomInformationViewModel _updateRoomInformationViewModel;
        private readonly DeleteRoomInformationViewModel _deleteRoomInformationViewModel;
        private readonly IRoomService _roomService;

        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string TypeDescription { get; set; }
        public string TypeNote { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }

        public RelayCommand ShowUpdateRoomInformationViewCommand { get; set; }
        public RelayCommand ShowDeleteRoomInformationViewCommand { get; set; }

        public UpdateRoomInformationPopupView updateRoomInformationWin;

        public DeleteRoomInformationPopupView deleteRoomInformationWin;

        public RoomInformationItemViewModel(UpdateRoomInformationViewModel updateRoomInformationViewModel,
            DeleteRoomInformationViewModel deleteRoomInformationViewModel, IRoomService roomService)
        {
            _updateRoomInformationViewModel = updateRoomInformationViewModel;
            _deleteRoomInformationViewModel = deleteRoomInformationViewModel;
            _roomService = roomService;
            ShowUpdateRoomInformationViewCommand = new RelayCommand(async o => await ShowUpdateWindow(o), o => true);
            ShowDeleteRoomInformationViewCommand = new RelayCommand(ShowDeleteWindow, o => true);
        }

        private async Task ShowUpdateWindow(object roomId)
        {
            if (roomId != null)
            {
                await _updateRoomInformationViewModel.GetRoomTypesAsync();
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
