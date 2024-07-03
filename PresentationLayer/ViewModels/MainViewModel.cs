using PresentationLayer.Commands;
using PresentationLayer.Services;
using PresentationLayer.Views;

namespace PresentationLayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navService;

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

        public MainViewModel(INavigationService navService) 
        {
            _navService = navService;
            NavigateToManageCustomerViewCommand = new RelayCommand(o => { Navigation.NavigateTo<ListCustomersViewModel>(); }, o => true);
        }


    }
}
