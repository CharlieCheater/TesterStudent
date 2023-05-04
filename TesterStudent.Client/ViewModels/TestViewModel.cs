using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterStudent.Client.Controls;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Models;

namespace TesterStudent.Client.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        private Variant _variant;
        private int _selectedId;
        public int SelectedId
        {
            get => _selectedId;
            set
            {
                if (_selectedId != value)
                {
                    _selectedId = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<OneCorrectExercise> Views { get; set; } = new List<OneCorrectExercise>();
        public TestViewModel(Variant variant)
        {

            _variant = variant;
            SelectViewCommand = new RelayCommand(SelectView);
        }
        public async Task Initialize()
        {
            var exercises = await App.Client.CallAsync<List<Exercise>>(Enums.HttpMethodTypes.Get, "test/getexercises", new Parameter("id", _variant.Id.ToString()));
            int counter = 1;
            foreach (var exercise in exercises.Data)
            {
                OneCorrectExercise view = new(exercise)
                {
                    Id = counter++,
                };
                Views.Add(view);
            }
        }
        public RelayCommand SelectViewCommand { get; private set; }
        public void SelectView(object obj)
        {
            int id = (int)obj;

        }
    }
}
