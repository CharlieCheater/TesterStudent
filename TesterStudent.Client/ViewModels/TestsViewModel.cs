using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Enums;
using TesterStudent.Client.Models;
using TesterStudent.Client.Views.Test;

namespace TesterStudent.Client.ViewModels;

public class TestsViewModel : ViewModelBase
{
    private Page _page;
    public TestsViewModel(Page page)
    {
        _page = page;
    }
    public async Task Initialize()
    {
        await Refresh();
    }
    public async Task Refresh()
    {
        var tests = await App.Client.CallAsync<List<Test>>(HttpMethodTypes.Get, "test/gettests", null);
        Tests.Clear();
        foreach (var test in tests.Data)
        {
            Tests.Add(test);
        }
    }
    public ObservableCollection<Test> Tests { get; set; } = new ObservableCollection<Test>();

    private RelayCommand _goBack;
    public RelayCommand GoBack
    {
        get => _goBack ??= new(obj =>
        {
            var page = obj as Page;
            page.NavigationService.GoBack();
        });
    }

    private RelayCommand _checkTest;
    public RelayCommand CheckTest
    {
        get => _checkTest ??= new(obj =>
        {
            var variant = obj as Variant;
            _page.NavigationService.Navigate(new TestPage(variant));
        });
    }
}
