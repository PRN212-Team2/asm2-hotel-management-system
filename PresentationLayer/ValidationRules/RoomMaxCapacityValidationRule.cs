using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class RoomMaxCapacityValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string capacityString = value as string;

            if (string.IsNullOrEmpty(capacityString))
            {
                return new ValidationResult(false, "Room maximum capacity cannot be empty.");
            }

            if (!int.TryParse(capacityString, out int capacity))
            {
                return new ValidationResult(false, "Room maximum capacity must be a number.");
            }

            if (capacity <= 0)
            {
                return new ValidationResult(false, "Room maximum capacity must be greater than 0.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
