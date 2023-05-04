using System;
using System.Windows;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Services;
using TesterStudent.Client.ViewModels;
using TesterStudent.Client.Views.Main;

namespace TesterStudent.Client.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel _mainViewModel;
    public MainWindow()
    {
        _mainViewModel =new MainViewModel();
        InitializeComponent();
    }
    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        Navigation.Service = mainFrame.NavigationService;
        await _mainViewModel.InitializeAsync();
        DataContext = _mainViewModel;
        Navigation.Navigate(new MainMenuPage(), new MainMenuViewModel(_mainViewModel.User));
    }
}
//Loaded += MainWindow_Loaded;
//    }
//
//    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
//{
//    await _mainViewModel.InitializeAsync();
//    Navigation.Service = mainFrame.NavigationService;
//    DataContext = _mainViewModel;
//    Navigation.Navigate(new MainMenuPage(_mainViewModel.User));
//}