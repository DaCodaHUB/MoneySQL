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
            var pass = NewpasswordTxt.SecurePassword;

            if (Temppass.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Your Password didn't meet requirement");
                return;
            }

            var confirm = NewconfirmTxt.SecurePassword;
            if (!SecurePasswordBox.ConvertToUnsecureString(pass)
                .Equals(SecurePasswordBox.ConvertToUnsecureString(confirm)))
            {
                MessageBox.Show("Confirm password doesn't match");
                return;
            }

            if (VerifyTxt.BorderBrush.ToString().Equals("#FFF44336"))
            {
                MessageBox.Show("Verify Code is incorrect.");
                return;
            }

            SqlConnect.UpdateMode(_user, "SetPassword", pass);
            MessageBox.Show("Your password is reset.");
            Close();
        }


        private void NewpasswordTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(NewpasswordTxt.SecurePassword),
                "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            Temppass.SelectedText = result;
        }

        private void NewconfirmTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var result = Regex.Replace(SecurePasswordBox.ConvertToUnsecureString(NewconfirmTxt.SecurePassword), "[a-z]",
                "a");
            result = Regex.Replace(result, "[A-Z]", "A");
            result = Regex.Replace(result, "[0-9]", "0");
            Tempconfirm.SelectedText = result;
        }
    }
}