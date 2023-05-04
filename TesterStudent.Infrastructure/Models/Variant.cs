namespace TesterStudent.Infrastructure.Models;

public class Variant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
    public List<Exercise> Exercise { get; set; }
    public List<UserVariant> Users { get; set; }

}
