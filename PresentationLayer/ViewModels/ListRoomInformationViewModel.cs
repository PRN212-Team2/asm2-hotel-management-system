using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class ListRoomInformationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomInformationService;
        private readonly CreateRoomInformationViewModel _createroomInformationViewModel;
        private readonly UpdateRoomInformationViewModel _updateroomInformationViewModel;
        private readonly DeleteRoomInformationViewModel _deleteroomInformationViewModel;
        private ObservableCollection<RoomInformationDetailsViewModel> _roomInformation;
        public ObservableCollection<RoomInformationDetailsViewModel> RoomInformation
        {
            get => _roomInformation;
            set
            {
                _roomInformation = value;
                OnPropertyChanged(nameof(RoomInformation));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterRoomInformation(value);
            }
        }

        public RelayCommand ShowCreateRoomInformationWindow { get; set; }

        public ListRoomInformationViewModel(IRoomService roomInformationService,
            CreateRoomInformationViewModel createRoomInformationViewModel,
            UpdateRoomInformationViewModel updateRoomInformationViewModel,
            DeleteRoomInformationViewModel deleteRoomInformationViewModel)
        {
            _roomInformationService = roomInformationService;
            _createroomInformationViewModel = createRoomInformationViewModel;
            _updateroomInformationViewModel = updateRoomInformationViewModel;
            _deleteroomInformationViewModel = deleteRoomInformationViewModel;
            _createroomInformationViewModel.RoomInformationCreated += OnRoomInformationCreated;
            _deleteroomInformationViewModel.RoomInformationDeleted += OnRoomInformationDeleted;
            _updateroomInformationViewModel.RoomInformationUpdated += OnRoomInformationUpdated;
            ShowCreateRoomInformationWindow = new RelayCommand(ShowCreateWindow, o => true);
            SearchText = "";
        }

        private void ShowCreateWindow(object obj)
        {
            CreateRoomInformationPopupView createRoomInformationWin = new CreateRoomInformationPopupView(_createroomInformationViewModel);
            createRoomInformationWin.Show();
        }

        private async void OnRoomInformationCreated(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        private async void OnRoomInformationDeleted(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        private async void OnRoomInformationUpdated(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        private async void FilterRoomInformation(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                await GetRoomInformationAsync();
            }
            else
            {
                searchText = searchText.ToLowerInvariant(); // Ensure case-insensitive search

                await GetRoomInformationAsync();

                // Check if the search text is a valid number (for price filtering)
                if (decimal.TryParse(searchText, out decimal searchPrice))
                {
                    RoomInformation = new ObservableCollection<RoomInformationDetailsViewModel>(
                        RoomInformation.Where(c =>
                            c.RoomPricePerDay <= searchPrice
                        )
                    );
                }
                else
                {
                    RoomInformation = new ObservableCollection<RoomInformationDetailsViewModel>(
                        RoomInformation.Where(c =>
                            c.RoomNumber.ToLowerInvariant().Contains(searchText)
                        )
                    );
                }
            }
        }

        public async Task GetRoomInformationAsync()
        {
            var rooms = await _roomInformationService.GetRoomInformationAsync();

            var roomInformationObservable = new ObservableCollection<RoomInformationDetailsViewModel>();
            foreach (var room in rooms)
            {
                var roomInformationDetail = new RoomInformationDetailsViewModel(_updateroomInformationViewModel, _deleteroomInformationViewModel, _roomInformationService);
                roomInformationDetail.RoomID = room.RoomID;
                roomInformationDetail.RoomNumber = room.RoomNumber;
                roomInformationDetail.RoomDetailDescription = room.RoomDetailDescription;
                roomInformationDetail.RoomMaxCapacity = room.RoomMaxCapacity;
                roomInformationDetail.RoomTypeID = room.RoomTypeID;
                roomInformationDetail.RoomStatus = room.RoomStatus;
                roomInformationDetail.RoomPricePerDay = room.RoomPricePerDay;
                roomInformationObservable.Add(roomInformationDetail);
            }

            RoomInformation = roomInformationObservable;
        }
    }
}
