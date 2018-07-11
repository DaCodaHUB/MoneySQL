using System;
using System.Windows;
using Banker.Database;
using System.Net.Mail;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace Banker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TextFields();
        }

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;
            var pass = PasswordTxt.SecurePassword;

            var result = SqlConnect.VerifyUser(username, pass);
            if (!result.Valid)
                MessageBox.Show($@"Username or password is incorrect");
            else
            {
                Hide();
                var incomeForm = new IncomeForm(result.Id);
                incomeForm.Closed += (s, args) => Close();
                incomeForm.Show();
            }
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            var signUpForm = new SignUp();
            signUpForm.Closed += (s, args) => Show();
            signUpForm.Show();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            var emailForm = new EmailForm();
            emailForm.Show();
        }
    }
}