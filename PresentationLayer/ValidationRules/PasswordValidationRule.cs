using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Password is required.");
            }

            string password = value.ToString();

            if (password.Length < 4)
            {
                return new ValidationResult(false, "Password must be at least 4 characters long.");
            }

            bool hasNumber = password.Any(c => char.IsNumber(c));
            bool hasSpecial = password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c));

            if (!hasNumber || !hasSpecial)
            {
                return new ValidationResult(false,
                  "Password must contain at least one number, and one special character.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
