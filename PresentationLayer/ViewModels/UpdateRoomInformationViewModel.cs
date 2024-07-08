using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class UpdateRoomInformationViewModel
    {
        private readonly IRoomInformationService _roomInformationService;
        private readonly IMapper _mapper;
        public event EventHandler RoomInformationUpdated;
        public RelayCommand UpdateRoomInformationCommand { get; set; }

        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }

        public UpdateRoomInformationViewModel(IRoomInformationService roomInformationService, IMapper mapper)
        {
            _roomInformationService = roomInformationService;
            _mapper = mapper;
            UpdateRoomInformationCommand = new RelayCommand(async o => await UpdateRoomInformationAsync(o), CanExecuteUpdateRoomInformationCommand);
        }

        public async Task LoadRoomInformationDetail(int id)
        {
            var room = await _roomInformationService.GetRoomInformationByIdAsync(id);
            if (room != null)
            {
                RoomID = room.RoomID;
                RoomNumber = room.RoomNumber;
                RoomDetailDescription = room.RoomDetailDescription;
                RoomMaxCapacity = room.RoomMaxCapacity;
                RoomTypeID = room.RoomTypeID;
                RoomStatus = room.RoomStatus;
                RoomPricePerDay = room.RoomPricePerDay;
            }
        }

        private bool CanExecuteUpdateRoomInformationCommand(object obj)
        {
            if (
                string.IsNullOrWhiteSpace(RoomNumber) ||
                string.IsNullOrWhiteSpace(RoomDetailDescription) ||
                RoomMaxCapacity <= 0 ||
                RoomTypeID <= 0 ||
                RoomPricePerDay <= 0)
            {
                return false;
            }
            return true;
        }

        private async Task UpdateRoomInformationAsync(object obj)
        {
            var roomToUpdate = _mapper.Map<UpdateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>(this);
            await _roomInformationService.UpdateRoomAsync(roomToUpdate, RoomID);
            MessageBox.Show("Room Information Updated Successfully");
            RoomInformationUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
