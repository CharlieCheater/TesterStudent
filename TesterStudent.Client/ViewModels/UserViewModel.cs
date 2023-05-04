namespace TesterStudent.Client.ViewModels;

    public class UserViewModel : ViewModelBase
    {
	private string _userName;

	public string Username
	{
		get { return _userName; }
		set
		{
			if (_userName != value)
			{
				_userName = value;
				OnPropertyChanged();
			}
		}
	}

	private string _firstName;

	public string Firstname
	{
		get { return _firstName; }
		set
		{
			if (_firstName != value)
			{
				_firstName = value;
				OnPropertyChanged();
			}
		}
	}
	private string _lastName;

	public string Lastname
	{
		get { return _lastName; }
		set
		{
			if (_lastName != value)
			{
				_lastName = value;
				OnPropertyChanged();
			}
		}
	}


}
