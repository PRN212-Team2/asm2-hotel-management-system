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
    public class CreateRoomInformationViewModel
    {
        private readonly IRoomService _roomInformationService;
        private readonly IMapper _mapper;
        public event EventHandler RoomInformationCreated;
        public RelayCommand CreateRoomInformationCommand { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public bool RoomStatus { get; set; } = true;
        public decimal RoomPricePerDay { get; set; }

        public CreateRoomInformationViewModel(IRoomService roomInformationService, IMapper mapper)
        {
            _roomInformationService = roomInformationService;
            _mapper = mapper;
            CreateRoomInformationCommand = new RelayCommand(async o => await CreateRoomInformationAsync(o), CanExecuteCreateRoomInformationCommand);
        }

        private bool CanExecuteCreateRoomInformationCommand(object obj)
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

        private async Task CreateRoomInformationAsync(object obj)
        {
            var roomToCreate = _mapper.Map<CreateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>(this);
            await _roomInformationService.CreateRoomAsync(roomToCreate);
            MessageBox.Show("Room Created Successfully!");
            RoomInformationCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
