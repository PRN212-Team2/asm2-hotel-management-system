using PresentationLayer.Commands;
using PresentationLayer.Services;
using PresentationLayer.Views;

namespace PresentationLayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private readonly ListCustomersViewModel _listCustomersViewModel;

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

        public MainViewModel(INavigationService navService, ListCustomersViewModel listCustomersViewModel) 
        {
            _navService = navService;
            _listCustomersViewModel = listCustomersViewModel;
            NavigateToManageCustomerViewCommand = new RelayCommand(async o => await NavigateToManageCustomerView(o), o => true);
        }

        private async Task NavigateToManageCustomerView(object obj)
        {
            await _listCustomersViewModel.GetCustomersAsync();
            Navigation.NavigateTo<ListCustomersViewModel>();
        }
    }
}
