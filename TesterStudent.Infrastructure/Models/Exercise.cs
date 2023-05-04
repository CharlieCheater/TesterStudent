namespace TesterStudent.Infrastructure.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Description { get; set; }

    public int VariantId { get; set; }
    public Variant Variant { get; set; }
    public List<PossibleAnswer> Answers { get; set; }
}