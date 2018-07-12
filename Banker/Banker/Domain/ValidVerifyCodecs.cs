using System.Globalization;
using System.Windows.Controls;
using Banker.Database;

namespace Banker.Domain
{
    internal class ValidVerifyCodecs : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Field is required.");
            var code = (value ?? "").ToString();

            return !LuhnAlgorithm.CheckLuhn(code) ? new ValidationResult(false, "Verify code is incorrect.") : ValidationResult.ValidResult;
        }
    }
}
