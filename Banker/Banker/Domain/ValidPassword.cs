using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Banker.Domain
{
    class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Field is required.");
            var password = (value ?? "").ToString();

            if (!char.IsLetter(password[0]))
                return new ValidationResult(false, "Must start with a letter");

            if (!password.Any(char.IsUpper))
                return new ValidationResult(false, "Require at least one upper case character");

            if (!password.Any(char.IsDigit))
                return new ValidationResult(false, "Require at least one number");

            var letterAndNumber = new Regex("[^A-Za-z0-9 ]");
            if (!letterAndNumber.IsMatch(password))
                return new ValidationResult(false, "Require at least one special character");


            if (password.Length <= 6 || password.Length > 31)
                return new ValidationResult(false, "Must be 7 to 31 character in length");

            return ValidationResult.ValidResult;
        }
    }
}