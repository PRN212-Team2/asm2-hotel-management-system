using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using PresentationLayer.Views;
using RepositoryLayer.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomService;
        private readonly IBookingReservationService _bookingReservationService;
        private readonly RoomInformationDetailsViewModel _roomInformationDetailsViewModel;
        private readonly IMapper _mapper;

        private ObservableCollection<MakeReservationRoomItemViewModel> _rooms;
        public ObservableCollection<MakeReservationRoomItemViewModel> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public ObservableCollection<BasketItem> BasketItems { get; set; }

        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

        private int? _selectedRoomTypeID;
        public int? SelectedTypeID
        {
            get => _selectedRoomTypeID;
            set
            {
                _selectedRoomTypeID = value;
                OnPropertyChanged(nameof(SelectedTypeID));
                FilterRooms(value);
            }
        }

        public int CustomerID { get; set; }

        public decimal TotalPrice { get; set; }

        public RelayCommand MakeReservationCommand { get; set; }

        public RelayCommand ResetFilterCommand { get; set; }

        public MakeReservationViewModel(IRoomService roomService, 
           IBookingReservationService bookingReservationService , 
           RoomInformationDetailsViewModel roomInformationDetailsViewModel, 
           IMapper mapper)
        {
            _roomService = roomService;
            _bookingReservationService = bookingReservationService;
            _roomInformationDetailsViewModel = roomInformationDetailsViewModel;
            _mapper = mapper;
            BasketItems = BasketManager.GetBasketItems();
            MakeReservationCommand = new RelayCommand(async o => await MakeReservation(o), o => true);
            ResetFilterCommand = new RelayCommand(async o => await ResetFilter(o), o => true);
        }

        public async Task MakeReservation(object obj)
        {
            IReadOnlyList<BasketItemDTO> basketItems = _mapper
                .Map<IReadOnlyList<BasketItem>, IReadOnlyList<BasketItemDTO>>(BasketItems.ToList());
            await _bookingReservationService.MakeReservation(CustomerID, basketItems);
            BasketManager.BasketItems.Clear();
            MessageBox.Show("Reservation Created Successfully!");
        }

        public async Task GetRoomTypesAsync()
        {
            var roomTypes = await _roomService.GetRoomTypesAsync();
            RoomTypes = new ObservableCollection<RoomTypeDTO>(roomTypes);
        }

        private async void FilterRooms(int? selectedTypeId)
        {
            if (!selectedTypeId.HasValue)
            {
                await GetRoomsAsync();
            }
            else
            {
                await GetRoomsAsync();

                Rooms = new ObservableCollection<MakeReservationRoomItemViewModel>(
                    Rooms.Where(r => r.RoomTypeID == selectedTypeId.Value)
                );
            }
        }

        private async Task ResetFilter(object obj)
        {
            SelectedTypeID = null;
            await GetRoomsAsync();
        }

        public async Task GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsWithTypeAsync();

            var roomObservable = new ObservableCollection<MakeReservationRoomItemViewModel>();
            foreach (var room in rooms)
            {
                var roomItem = new MakeReservationRoomItemViewModel(_roomInformationDetailsViewModel);
                roomItem.RoomID = room.RoomID;
                roomItem.RoomNumber = room.RoomNumber;
                roomItem.RoomMaxCapacity = room.RoomMaxCapacity;
                roomItem.RoomPricePerDay = room.RoomPricePerDay;
                roomItem.RoomStatus = room.RoomStatus;
                roomItem.RoomTypeID = room.RoomTypeID;
                roomItem.RoomTypeName = room.RoomTypeName;
                roomObservable.Add(roomItem);
            }

            Rooms = roomObservable;
        }

    }
}
