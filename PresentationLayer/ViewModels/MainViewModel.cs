using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using PresentationLayer.Services;
using PresentationLayer.Views;

namespace PresentationLayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private readonly ListCustomersViewModel _listCustomersViewModel;
        private readonly ListBookingReservationHistoryViewModel _listBookingReservationHistoryViewModel;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAccountModel CurrentUser { get; set; }

        public INavigationService Navigation
        {
            get => _navService;
            set
            {
                _navService = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdmin;
        private bool _isCustomer;

        public bool IsAdmin 
        {  
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }
        public bool IsCustomer
        {
            get => _isCustomer;
            set
            {
                _isCustomer = value;
                OnPropertyChanged(nameof(IsCustomer));
            }
        }

        public RelayCommand NavigateToManageCustomerViewCommand { get; set; }

        public RelayCommand NavigateToListBookingReservationHistoyrViewCommand { get; set; }

        public MainViewModel(INavigationService navService, 
            ListCustomersViewModel listCustomersViewModel,
            ListBookingReservationHistoryViewModel listBookingReservationHistoryViewModel,
            IUserService userService,
            IMapper mapper) 
        {
            _navService = navService;
            _listCustomersViewModel = listCustomersViewModel;
            _listBookingReservationHistoryViewModel = listBookingReservationHistoryViewModel;
            _userService = userService;
            _mapper = mapper;
            NavigateToManageCustomerViewCommand = new RelayCommand(async o => await NavigateToManageCustomerView(o), o => true);
            NavigateToListBookingReservationHistoyrViewCommand = new RelayCommand(async o => await NavigateToBookingReservationHistoryView(o), o => true);
        }

        private async Task NavigateToManageCustomerView(object obj)
        {
            await _listCustomersViewModel.GetCustomersAsync();
            Navigation.NavigateTo<ListCustomersViewModel>();
        }

        private async Task NavigateToBookingReservationHistoryView(object obj)
        {
            var customerId = CurrentUser.Id;
            await _listBookingReservationHistoryViewModel.GetBookingReservationsAsync(customerId);
            Navigation.NavigateTo<ListBookingReservationHistoryViewModel>();
        }

        public async Task LoadCurrentUser()
        {
            if(Thread.CurrentPrincipal
                    .IsInRole(UserRole.Admin.ToString())) 
            {
                CurrentUser = new UserAccountModel();
                CurrentUser.EmailAddress = Thread.CurrentPrincipal.Identity.Name;
                CurrentUser.Role = UserRole.Admin;
                
            }
            else if (Thread.CurrentPrincipal
                    .IsInRole(UserRole.Customer.ToString()))
            {
                var user = await _userService.GetUserByEmailAsync(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                    CurrentUser = _mapper.Map<UserDTO, UserAccountModel>(user);
                    CurrentUser.Role = UserRole.Customer;
                    IsAdmin = CurrentUser.Role == UserRole.Admin;
                }
            }
            IsAdmin = CurrentUser.Role == UserRole.Admin;
            IsCustomer = CurrentUser.Role == UserRole.Customer;
        }
    }
}
