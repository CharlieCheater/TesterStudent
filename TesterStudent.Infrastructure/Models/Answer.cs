namespace TesterStudent.Infrastructure.Models;

public class Answer
{
    public int Id { get; set; }
    public string? Value { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } 
    public int? PossibleAnswerId { get; set; }
    public PossibleAnswer? PossibleAnswer { get; set; }
}