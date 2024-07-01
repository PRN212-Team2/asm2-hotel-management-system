using PresentationLayer.Commands;
using PresentationLayer.Services;

namespace PresentationLayer.ViewModel
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

        public RelayCommand NavigateToExampleViewCommand { get; set; }

        public MainViewModel(INavigationService navService) 
        {
            _navService = navService;
            NavigateToExampleViewCommand = new RelayCommand(
                o => { Navigation.NavigateTo<ExampleViewModel>(); }, 
                o => true
            );
        }


    }
}
