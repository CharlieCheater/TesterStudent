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
using TesterStudent.Client.Models;
using TesterStudent.Client.ViewModels;

namespace TesterStudent.Client.Views.Test
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private TestViewModel _viewModel;
        public TestPage(Variant variant)
        {
            _viewModel = new TestViewModel(variant);
            InitializeComponent();
        }
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await _viewModel.Initialize();
            DataContext = _viewModel;
        }
    }
}
