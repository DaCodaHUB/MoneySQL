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
    class ValidUserName : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Field is required.");
            var username = (value ?? "").ToString();
            if (!char.IsLetter(username[0]))
                return new ValidationResult(false, "Must start with a letter");

            var letterAndNumber = new Regex("[^A-Za-z0-9]");
            if (letterAndNumber.IsMatch(username))
                return new ValidationResult(false, "Not allow special characters");

            if (username.Length < 4 || username.Length > 31)
                return new ValidationResult(false, "Must be 5 to 30 character in length");

            return ValidationResult.ValidResult;
        }
    }
}