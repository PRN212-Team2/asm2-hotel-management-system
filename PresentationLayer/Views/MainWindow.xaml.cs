using BusinessServiceLayer.DTOs;
using PresentationLayer.ViewModels;
using BusinessServiceLayer.Services;
using System.Windows;

namespace PresentationLayer.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private MainViewModel _mainViewModel;

    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        DataContext = _mainViewModel;
    }
}