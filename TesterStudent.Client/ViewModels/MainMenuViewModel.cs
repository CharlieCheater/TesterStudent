using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Services;
using TesterStudent.Client.Views.Main;
using TesterStudent.Client.Views.Teacher;

namespace TesterStudent.Client.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    public MainMenuViewModel(User user)
    {
        User = user;
        CheckTests = new RelayCommand(NavigateToTests);
        CheckUsers = new RelayCommand(NavigateToUsers);
    }
    private User _user;
    public User User
    {
        get => _user;
        set
        {
            if (value == _user) return;
            _user = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand CheckUsers { get; set; }
    public RelayCommand CheckTests { get; private set; }
    private void NavigateToTests(object obj)
    {
        Navigation.Navigate(new TestsPage());
    }
    private void NavigateToUsers(object obj)
    {
        Navigation.Navigate(new UsersPage());
    }
    public Visibility TeacherVisibility => User.Roles.Contains("Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
}
