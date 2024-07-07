using BusinessServiceLayer.DTOs;
using PresentationLayer.ViewModels;
using BusinessServiceLayer.Services;
using System.Windows;
using System.ComponentModel;

namespace PresentationLayer.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private MainViewModel _mainViewModel;
    private readonly LoginViewModel _loginViewModel;
    private readonly LoginView _loginView;

    public MainWindow(MainViewModel mainViewModel, LoginViewModel loginViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        _loginViewModel = loginViewModel;
        DataContext = _mainViewModel;
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        _loginViewModel.IsViewVisible = true;
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        _loginViewModel.IsViewVisible = true;
        this.Close();
    }
}