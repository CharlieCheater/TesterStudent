namespace TesterStudent.Infrastructure.Models;

public class UserVariant
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int VariantId { get; set; }
    public Variant Variant { get; set; }

    public float Score { get; set; }
}
