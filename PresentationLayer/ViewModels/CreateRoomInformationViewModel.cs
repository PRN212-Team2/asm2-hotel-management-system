using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class CreateRoomInformationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomInformationService;
        private readonly IMapper _mapper;
        public event EventHandler RoomInformationCreated;
        public RelayCommand CreateRoomInformationCommand { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public bool RoomStatus { get; set; } = true;
        public decimal RoomPricePerDay { get; set; }
        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

        private int? _roomTypeID;
        public int? RoomTypeID
        {
            get => _roomTypeID;
            set
            {
                _roomTypeID = value;
                OnPropertyChanged(nameof(RoomTypeID));
            }
        }

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

        public async Task GetRoomTypesAsync()
        {
            var roomTypes = await _roomInformationService.GetRoomTypesAsync();
            RoomTypes = new ObservableCollection<RoomTypeDTO>(roomTypes);
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
