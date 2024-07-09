using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ValidationRules
{
    public class RoomPricePerDayValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string priceString = value as string;

            if (string.IsNullOrEmpty(priceString))
            {
                return new ValidationResult(false, "Room price per day cannot be empty.");
            }

            if (!decimal.TryParse(priceString, out decimal price))
            {
                return new ValidationResult(false, "Room price per day must be a decimal number.");
            }

            if (price <= 0)
            {
                return new ValidationResult(false, "Room price per day must be greater than 0.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
