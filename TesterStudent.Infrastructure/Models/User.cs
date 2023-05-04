namespace TesterStudent.Infrastructure.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Password { get; set; }
    public List<Answer> Answers { get; set; }
    public List<UserRole> Roles { get; set; }
    public List<UserVariant> Variants { get; set; }
    public DateTime CreatedAt { get; set; }
}