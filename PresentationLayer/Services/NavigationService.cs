using PresentationLayer.ViewModels;

namespace PresentationLayer.Services
{
    public class NavigationService : ViewModelBase, INavigationService
    {
        private ViewModelBase _currentView;

        /// <summary>
        /// The current view model that is bound with a view
        /// </summary>
        public ViewModelBase CurrentView
        {
            get => _currentView;
            private set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private readonly Func<Type, ViewModelBase> _viewModelFactory;

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory) 
        {
            _viewModelFactory = viewModelFactory;
        }

        /// <summary>
        /// Change the current view model.
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
