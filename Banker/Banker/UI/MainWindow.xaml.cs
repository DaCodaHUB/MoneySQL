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
        private readonly SqlConnect _sql;

        public MainWindow()
        {
            InitializeComponent();
            _sql = new SqlConnect();
            DataContext = new TextFields();
        }

        

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;
            var pass = PasswordTxt.SecurePassword;
            
            var result = _sql.VerifyUser(username, pass);
            if (!result.Valid)
                MessageBox.Show($@"This {username} is not existed or password is incorrect");
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
            //            var result = new RandomListData().generate();
            //            foreach (var v in result)
            //            {
            //                Debug.WriteLine(v.ToString());
            //            }

            var signUpForm = new SignUp(_sql);
            signUpForm.Show();
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}