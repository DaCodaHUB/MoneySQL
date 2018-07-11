using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();

            DataContext = new TextFields();
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;
            var email = emailTxt.Text;

            if (emailTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Email doesn't meet requirement");
                return;
            }

            if (UsernameTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Username doesn't meet requirement");
                return;
            }

            var pass = passwordTxt.SecurePassword;

//            var regexPass = new Regex("^((?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()])).{7,30}$");
            if (temppass.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Your Password didn't meet requirement");
                return;
            }

            var confirm = confirmTxt.SecurePassword;
            if (!SecurePasswordBox.ConvertToUnsecureString(pass)
                .Equals(SecurePasswordBox.ConvertToUnsecureString(confirm)))
            {
                MessageBox.Show("Confirm password doesn't match");
                return;
            }


            var replayCode = SqlConnect.CreateNewUser(username, pass, email);

            switch (replayCode)
            {
                case 1:
                    MessageBox.Show($@"You can't create {username} because it was existed");
                    break;
                default:
                    MessageBox.Show($@"Congratulation, username {username} is created");
                    break;
            }
            Close();
        }

        private void PasswordTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(passwordTxt.SecurePassword), "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            temppass.SelectedText = result;
        }

        private void ConfirmTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(confirmTxt.SecurePassword), "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            tempconfirm.SelectedText = result;
        }
    }
}