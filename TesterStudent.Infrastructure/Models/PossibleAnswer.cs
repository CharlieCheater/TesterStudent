namespace TesterStudent.Infrastructure.Models;

public class PossibleAnswer
{
    public int Id { get; set; }
    public string Value { get; set; }
    public bool IsCorrect { get; set; }
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }
    public List<Answer> Answers { get; set; }
}