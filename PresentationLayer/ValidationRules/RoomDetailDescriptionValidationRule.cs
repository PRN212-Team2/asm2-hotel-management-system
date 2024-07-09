using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class RoomDetailDescriptionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string description = value as string;

            if (string.IsNullOrEmpty(description))
            {
                return new ValidationResult(false, "Room description cannot be empty.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
