using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.Database
{
    public class LuhnAlgorithm
    {
        //this function will return check digit
        //it will double the every alternate digit starting from last one, 
        //in our case it will take "1" and than other alternate digit.
        //sum of all the doubled digit and excluded digits
        //do modulus 10 on sum and the reminder-10 will give u the check digit and that's it
        public static string GetLuhnCheckDigit(string number)
        {
            var time = DateTime.Now;
            var skipDigit1 = (time.Day + time.Month + time.DayOfYear) % 10;
            var skipDigit2 = (time.Year + time.Month + time.Day) % 10;
            var mins = time.Minute.ToString("D2").ToCharArray();
            var days = time.Day.ToString("D2").ToCharArray();
            var sum = 0;
            var alt = true;

            var digits = number.ToCharArray().ToList();
            digits.Insert(2, mins[0]); // Shift right index 3
            digits.Insert(5, mins[1]); // Shift right index 7
            digits.Insert(1, days[0]); // index 1
            digits.Insert(6, days[1]); // index 6

            for (var i = digits.Count - 1; i >= 0; i--)
            {
                if (i == skipDigit1 || i == skipDigit2) continue;
                var curDigit = (digits[i] - 48);


                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                        curDigit -= 9;
                }

                sum += curDigit;
                alt = !alt;
            }

            var lastDigit = (sum % 10) == 0 ? "0" : (10 - (sum % 10)).ToString();

            return string.Join("", digits.ToArray()) + lastDigit;
        }

        public static bool checkLuhn(string Number)
        {
            var time = DateTime.Now;
            var skipDigit1 = (time.Day + time.Month + time.DayOfYear) % 10;
            var skipDigit2 = (time.Year + time.Month + time.Day) % 10;
            var mins = time.Minute;
            var days = time.Day;

            var total = 0;
            var alt = false;

            var digits = Number.ToCharArray().ToList();
            var twodigitsMin = int.Parse("" + digits[3] + digits[7]);
            var twodigitsDay = int.Parse("" + digits[1] + digits[6]);

            if (twodigitsDay != days)
                return false;

            if (mins < twodigitsMin)
                mins += 60;
            if (mins % twodigitsMin > 5)
                return false;

            for (var i = digits.Count - 1; i >= 0; i--)
            {
                if (i == skipDigit1 || i == skipDigit2) continue;
                var curDigit = (int) char.GetNumericValue(digits[i]);
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                        curDigit -= 9;
                }

                total += curDigit;
                alt = !alt;
            }

            return total % 10 == 0;
        }
    }
}