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
    public class EmailAddressValidationRule : ValidationRule
    {
        private readonly Regex _emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}))$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(false, "Email Address is required.");
            }

            if (!_emailRegex.IsMatch(stringValue))
            {
                return new ValidationResult(false, "Invalid Email Address format.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
