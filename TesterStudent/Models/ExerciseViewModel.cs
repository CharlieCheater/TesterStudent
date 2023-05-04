namespace TesterStudent.Models
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool HasMultipleCorrectAnswers { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();
    }
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
