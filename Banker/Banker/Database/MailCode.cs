using System;
using System.Net.Mail;

namespace Banker.Database
{
    public class MailCode
    {
        public MailCode(string email)
        {
            var verifyCode = GenerateCode();

            var to = email;
            const string @from = "bankerserver@gmail.com";
            const string subject = "Reset Code";
            var body = "You wish to reset your password. Use this code to verify the process: " + verifyCode;
            var message = new MailMessage(from, to, subject, body);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("bankerserver@gmail.com", "passtest123456")
            };


            try
            {
                client.Send(message);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(@"Exception caught in CreateTimeoutTestMessage(): {0}",
                    ex);
            }
        }

        private static string GenerateCode()
        {
            Random rdm = new Random();
            int value = rdm.Next(10000, 100000);
            string text = value.ToString("D5");

            return LuhnAlgorithm.GetLuhnCheckDigit(text);
        }
    }
}