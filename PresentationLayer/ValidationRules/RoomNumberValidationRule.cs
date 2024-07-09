using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class RoomNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string roomNumber = value as string;

            if (string.IsNullOrEmpty(roomNumber))
            {
                return new ValidationResult(false, "Room number cannot be empty.");
            }

            if (!int.TryParse(roomNumber, out int number))
            {
                return new ValidationResult(false, "Room number must be a number.");
            }

            if (roomNumber.Length != 4)
            {
                return new ValidationResult(false, "Room number must be 4 digits long.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
