using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
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
        private readonly CustomerProfileViewModel _customerProfileViewModel;
        private readonly ListReportStatisticsViewModel _listReportStatisticViewModel;
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
        public RelayCommand NavigateToListBookingReservationHistoryViewCommand { get; set; }
        public RelayCommand NavigateToCustomerProfileViewCommand { get; set; }
        public RelayCommand NavigateToReportStatisticViewCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        public MainViewModel(INavigationService navService, 
            ListCustomersViewModel listCustomersViewModel,
            ListBookingReservationHistoryViewModel listBookingReservationHistoryViewModel,
            CustomerProfileViewModel customerProfileViewModel,
            ListReportStatisticsViewModel listReportStatisticsViewModel,
            IUserService userService,
            IMapper mapper) 
        {
            _navService = navService;
            _listCustomersViewModel = listCustomersViewModel;
            _listBookingReservationHistoryViewModel = listBookingReservationHistoryViewModel;
            _customerProfileViewModel = customerProfileViewModel;
            _listReportStatisticViewModel = listReportStatisticsViewModel;
            _userService = userService;
            _mapper = mapper;
            NavigateToManageCustomerViewCommand = new RelayCommand(
                async o => await NavigateToManageCustomerView(o), o => true);
            NavigateToListBookingReservationHistoryViewCommand = new RelayCommand(
                async o => await NavigateToBookingReservationHistoryView(o), o => true);
            NavigateToCustomerProfileViewCommand = new RelayCommand(
                async o => await NavigateToCustomerProfileView(o), o => true);
            NavigateToReportStatisticViewCommand = new RelayCommand (
                async o => await NavigateToReportStatisticView(o), o => true);
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

        public async Task NavigateToCustomerProfileView(object obj)
        {
            await _customerProfileViewModel.loadProfileFromCurrentUser(CurrentUser.EmailAddress);
            Navigation.NavigateTo<CustomerProfileViewModel>();
        }

        public async Task NavigateToReportStatisticView(object obj)
        {
            await _listReportStatisticViewModel.GetBookingReservationsAsync();
            Navigation.NavigateTo<ListReportStatisticsViewModel>();
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

        public void Logout(object obj)
        {
            this.CurrentUser = null;
        }
    }
}
