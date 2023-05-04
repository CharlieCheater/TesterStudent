using System.Threading.Tasks;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;

namespace TesterStudent.Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    private User _user;
    private bool _initialized;
    public MainViewModel()
    {
    }
    public async Task InitializeAsync()
    {
        if (_initialized) return;
        var result = await App.Client.CallAsync<User>(HttpMethodTypes.Get, "account");
        User = result.Data;

        _initialized = true;
    }
    public User User
    {
        get => _user;
        set
        {
            if(value == _user) return;
            _user = value;
            OnPropertyChanged();
        }
    }

}
