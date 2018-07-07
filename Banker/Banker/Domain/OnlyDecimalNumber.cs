using System.Globalization;
using System.Windows.Controls;

namespace Banker.Domain
{
    public class OnlyDecimalNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!decimal.TryParse((value ?? "").ToString(), out var result))
            {
                return new ValidationResult(false, "Only Number");
            }
            return result < 0 ? new ValidationResult(false, "Number has to be positive") : ValidationResult.ValidResult;
        }
    }
}