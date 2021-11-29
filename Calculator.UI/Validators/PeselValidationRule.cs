using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Calculator.UI.Validators
{
    public class PeselValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pesel = Convert.ToString(value);
            
            if (string.IsNullOrEmpty(pesel))
                return new ValidationResult(false, $"Value cannot be coverted to string.");

            bool correctLenght;
            if (pesel.Count() == 11)
            {
                correctLenght = true;
            }
            else
                correctLenght = false;

            bool digital = containsOnlyDigits(pesel);
            return correctLenght && digital ? new ValidationResult(true, null) : new ValidationResult(false, "Invalid data format");
        }

        bool containsOnlyDigits(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
