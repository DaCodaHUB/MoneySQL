using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Banker.Database;

namespace Banker.Domain
{
    class ValidVerifyCodecs : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((value ?? "").ToString()))
                return new ValidationResult(false, "Field is required.");
            var code = (value ?? "").ToString();

            return !LuhnAlgorithm.checkLuhn(code) ? new ValidationResult(false, "Verify code is incorrect.") : ValidationResult.ValidResult;
        }
    }
}
