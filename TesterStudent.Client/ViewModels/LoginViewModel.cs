using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Views;
using TesterStudent.Client.Views.Auth;

namespace TesterStudent.Client.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if (value != _username)
            {
                _username = value;
                OnPropertyChanged();
            }
        }
    }
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            if (value != _password)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }

    private Result<string> _result;

    public Result<string> Result
    {
        get => _result;
        set
        {
            if (value != _result)
            {
                _result = value;
                OnPropertyChanged();
            }
        }
    }

    private RelayCommand _login;
    public RelayCommand Login
    {
        get => _login ??= new RelayCommand( async obj =>
        {
            Result = await App.Client.CallAsync<string>(HttpMethodTypes.Post, "Account", new LoginData(Username, Password));
            if (Result.Success)
            {
                var window = App.Current.MainWindow;
                App.Current.MainWindow = new MainWindow();
                window.Close();
                App.Current.MainWindow.Show();
            }
        }, (obj) => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)));
    }

    private RelayCommand _register;
    public RelayCommand Register
    {
        get => _register ??= new RelayCommand(async obj =>
        {
            var page = obj as Page;
            page?.NavigationService.Navigate(new RegistrationPage());
        });
    }
}

internal class LoginData
{
    public LoginData(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
}