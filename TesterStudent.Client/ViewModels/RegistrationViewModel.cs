using System.Windows;
using System.Windows.Controls;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Views;

namespace TesterStudent.Client.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
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
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (value != _firstname)
                {
                    _firstname = value;
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

        private RelayCommand _register;
        public RelayCommand Register
        {
            get => _register ??= new RelayCommand(async obj =>
            {
                Result = await App.Client.CallAsync<string>(HttpMethodTypes.Post, "Account/register", 
                    new RegistrationData(Username, Password, Lastname, Firstname));
                if (Result.Success)
                {
                    var window = App.Current.MainWindow;
                    App.Current.MainWindow = new MainWindow();
                    window.Close();
                    App.Current.MainWindow.Show();
                }
            }, (obj) => Validate());
        }

        private RelayCommand _login;
        public RelayCommand Login
        {
            get => _login ??= new RelayCommand(async obj =>
            {
                var page = obj as Page;
                page?.NavigationService.GoBack();
            });
        }
        public bool Validate()
        {
            if (!(string.IsNullOrEmpty(Username)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(Lastname)
                || string.IsNullOrEmpty(Firstname)))
            {
                return true;
            }
            return false;
        }
    }

    internal class RegistrationData
    {
        public RegistrationData(string username, string password, string lastname, string firstname)
        {
            Username = username;
            Password = password;
            Lastname = lastname;
            Firstname = firstname;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
    }
}
