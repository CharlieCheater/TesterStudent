using System.ComponentModel.DataAnnotations;

namespace TesterStudent.Models;

public class RegistrationViewModel : LoginViewModel
{
    [Required]
    public string Lastname { get; set; }
    [Required]
    public string Firstname { get; set; }
}
