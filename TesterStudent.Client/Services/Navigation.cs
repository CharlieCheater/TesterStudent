using System.Windows.Controls;
using System.Windows.Navigation;

namespace TesterStudent.Client.Services;

public class Navigation
{
    #region Singleton

    private static volatile Navigation instance;
    private static object syncRoot = new();
    private NavigationService _navService;
    private Navigation() { }

    private static Navigation Instance
    {
        get
        {

            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new Navigation();
                }
            }
            
            return instance;
        }
    }
    #endregion
    public static NavigationService Service
    {
        get { return Instance._navService; }
        set
        {
            if (Instance._navService != null)
            {
                Instance._navService.Navigated -= Instance._navService_Navigated;
            }

            Instance._navService = value;
            Instance._navService.Navigated += Instance._navService_Navigated;
        }
    }
    #region Public Methods

    public static void Navigate(Page page, object context)
    {
        if (Instance._navService == null || page == null)
        {
            return;
        }

        Instance._navService.Navigate(page, context);
    }

    public static void Navigate(Page page)
    {
        Navigate(page, null);
    }
    public static bool TryGoBack()
    {
        if(Instance._navService != null && Instance._navService.CanGoBack)
        {
            Instance._navService.GoBack();
            return true;
        }
        return false;
    }
    #endregion
    #region Private Methods

    void _navService_Navigated(object sender, NavigationEventArgs e)
    {
        var page = e.Content as Page;

        if (page == null)
        {
            return;
        }

        page.DataContext = e.ExtraData;
    }

    #endregion
}
