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

        public RelayCommand NavigateToManageCustomerViewCommand { get; set; }

        public MainViewModel(INavigationService navService, 
            ListCustomersViewModel listCustomersViewModel, 
            IUserService userService,
            IMapper mapper) 
        {
            _navService = navService;
            _listCustomersViewModel = listCustomersViewModel;
            _userService = userService;
            _mapper = mapper;
            NavigateToManageCustomerViewCommand = new RelayCommand(async o => await NavigateToManageCustomerView(o), o => true);
        }

        private async Task NavigateToManageCustomerView(object obj)
        {
            await _listCustomersViewModel.GetCustomersAsync();
            Navigation.NavigateTo<ListCustomersViewModel>();
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
                }
            }
        }
    }
}
