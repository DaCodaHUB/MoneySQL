using System.Text.RegularExpressions;
using System.Windows;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword
    {
        private readonly SqlConnect.User _user;

        public ResetPassword(SqlConnect.User user)
        {
            _user = user;

            InitializeComponent();
            DataContext = new TextFields();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            var pass = newpasswordTxt.SecurePassword;

            if (temppass.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Your Password didn't meet requirement");
                return;
            }

            var confirm = newconfirmTxt.SecurePassword;
            if (!SecurePasswordBox.ConvertToUnsecureString(pass)
                .Equals(SecurePasswordBox.ConvertToUnsecureString(confirm)))
            {
                MessageBox.Show("Confirm password doesn't match");
                return;
            }

            if (verifyTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Verify Code is incorrect.");
                return;
            }

//            MessageBox.Show(_username);
            SqlConnect.UpdateMode(_user, "SetPassword", pass);
            MessageBox.Show("Your password is reset.");
            Close();
        }


        private void NewpasswordTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(newpasswordTxt.SecurePassword),
                "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            temppass.SelectedText = result;
        }

        private void NewconfirmTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(newconfirmTxt.SecurePassword), "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            tempconfirm.SelectedText = result;
        }
    }
}