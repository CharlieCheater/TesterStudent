using System;
using System.Globalization;
using System.Windows.Controls;

namespace TesterStudent.Client.ValidationRules;

public class RequiredLengthValidationRule : ValidationRule
{
    public int RequiredLength { get; set; } = 6;
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var username = (string)value;
        if(username.Length < RequiredLength)
        {
            return new ValidationResult(false, $"Минимальная длина логина составляет {RequiredLength} символов");
        }
        return ValidationResult.ValidResult;
    }
}
