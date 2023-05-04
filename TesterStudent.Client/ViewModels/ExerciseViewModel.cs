using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterStudent.Client.Domain;
using TesterStudent.Client.Models;

namespace TesterStudent.Client.ViewModels
{
    public class ExerciseViewModel : ViewModelBase
    {
        private readonly Exercise _exercise;
		public List<Answer> Answers { get; set; }

		public string Description { get; set; }
		
		private Answer _selectedAnswer;
		public Answer SelectedAnswer
		{
			get { return _selectedAnswer; }
			set
			{
				if (_selectedAnswer != value)
				{
					_selectedAnswer = value;
					OnPropertyChanged();
				}
			}
		}
		public void Initialize()
		{
			Answers =  new List<Answer>();
            foreach (Answer answer in _exercise.Answers)
            {
                Answers.Add(answer);
            }
		}
		public ExerciseViewModel(Exercise exercise) 
        {
            _exercise = exercise;
        }
		private RelayCommand _selectAnswer;

        public RelayCommand SelectAnswer
		{
			get => _selectAnswer ??= new RelayCommand(async obj => SelectedAnswer = obj as Answer);

        }
    }
}
