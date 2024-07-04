using BusinessServiceLayer.Services;
using Microsoft.Extensions.Configuration;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly MainViewModel _mainViewModel;
        private readonly IConfiguration _configuration;

        public string Email { get; set; }
        public string Password { get; set; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        private bool _isViewVisible = true;
        public bool IsViewVisible 
        {
            get 
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible= value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        } 

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(IUserService userService, MainViewModel mainViewModel, IConfiguration configuration) 
        {
            _userService = userService;
            _mainViewModel = mainViewModel;
            _configuration = configuration;
            LoginCommand = new RelayCommand(async o => await LoginAsync(o), CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            if(string.IsNullOrEmpty(Email)) 
            {
                return false;
            }
            return true;
        }

        private async Task LoginAsync(object obj)
        {
            var adminEmail = _configuration["AdminEmail"];
            var adminPassword = _configuration["AdminPassword"];

            Password = ((PasswordBox) obj).Password;
            if(Email == adminEmail && Password == adminPassword)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Email), [UserRole.Admin.ToString()]);
                IsViewVisible = false;
                await _mainViewModel.LoadCurrentUser();
            }
            else
            {
                var user = await _userService.LoginAsync(Email, Password);
                if (user != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Email), [UserRole.Customer.ToString()]);
                    IsViewVisible = false;
                    await _mainViewModel.LoadCurrentUser();
                }
                else
                {
                    ErrorMessage = "Invalid Email or Password";
                }
            }
        }
    }
}
