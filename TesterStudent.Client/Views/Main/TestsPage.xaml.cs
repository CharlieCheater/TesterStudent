using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TesterStudent.Client.ViewModels;

namespace TesterStudent.Client.Views.Main
{
    /// <summary>
    /// Логика взаимодействия для TestsPage.xaml
    /// </summary>
    public partial class TestsPage : Page
    {
        private TestsViewModel _viewModel;

        public TestsPage()
        {
            InitializeComponent();
        }
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _viewModel = new TestsViewModel(this);
            await _viewModel.Initialize();
            DataContext = _viewModel;
        }
    }
}
