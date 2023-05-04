using System.Collections.Generic;
using System.Windows.Documents;

namespace TesterStudent.Client.Models;

public class Test
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Variant> AllowedVariants { get; set; }

    public Variant SelectedVariant { get; set; }
}
