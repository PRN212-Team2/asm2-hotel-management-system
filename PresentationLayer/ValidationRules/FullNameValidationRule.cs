using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class FullNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Full name is required.");
            }

            string fullName = value.ToString();
            int spaceIndex = fullName.IndexOf(' ');

            if (spaceIndex <= 0 || spaceIndex == fullName.Length - 1)
            {
                return new ValidationResult(false, "Full name must include a first name and a last name separated by a space.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
