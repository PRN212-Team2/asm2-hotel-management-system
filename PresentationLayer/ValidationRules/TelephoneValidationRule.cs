using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class TelephoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Telephone number is required.");
            }

            string phoneNumber = value.ToString();

            if (phoneNumber.Length != 10 || !phoneNumber.StartsWith("0"))
            {
                return new ValidationResult(false, "Phone number must be a 10-digit number starting with 0.");
            }

            // Optional: Check if all characters are digits (0-9)
            if (!int.TryParse(phoneNumber, out _))
            {
                return new ValidationResult(false, "Phone number must only contain digits.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
