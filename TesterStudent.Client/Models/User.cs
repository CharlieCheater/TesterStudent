using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace TesterStudent.Client.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public List<string> Roles { get; set; }
    public string Role => Roles.FirstOrDefault();
}
