using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Banker.Database
{
    public class MailCode
    {
        private string verifyCode;

        public MailCode(string email)
        {
            verifyCode = generateCode();

            string to = email;
            string from = "bankerserver@gmail.com";
            string subject = "Reset Code";
            string body = "You wish to reset your password. Use this code to verify the process: " + verifyCode;
            MailMessage message = new MailMessage(from, to, subject, body);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("bankerserver@gmail.com", "passtest123456");
            client.Send(message);

            try
            {
                client.Send(message);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("Exception caught in CreateTimeoutTestMessage(): {0}",
                      ex.ToString());
            }
        }

        private string generateCode()
        {
            Random rdm = new Random();
            int value = rdm.Next(100000, 1000000);
            string text = value.ToString("000000");
            return text;
        }

        public string VerifyString
        {
            get
            {
                return verifyCode;
            }
        }
    }
}
