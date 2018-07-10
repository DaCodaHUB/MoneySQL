using System;
using System.Globalization;
using System.Windows.Controls;

namespace Banker.Domain
{
    public class ValidEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Field is required.");
            var email = (value ?? "").ToString();
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Invalid Email.");
            }

            return ValidationResult.ValidResult;
        }
    }
}