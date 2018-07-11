using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
        private readonly SqlConnect _sql;

        public SignUp(SqlConnect Sql)
        {
            InitializeComponent();
            _sql = Sql;
            DataContext = new TextFields();
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;

            // Todo: Change to PasswordBox
            var pass = passwordTxt.SecurePassword;

            var replayCode = _sql.CreateNewUser(username, pass);

            switch (replayCode)
            {
                case 1:
                    MessageBox.Show($@"Username {username} is existed");
                    break;
                case 2:
                    MessageBox.Show(@"Username or password is empty");
                    break;
                case 3:
                    MessageBox.Show(@"Password needs at least 8 characters");
                    break;
                default:
                    MessageBox.Show($@"Username {username} is created");
                    break;
            }
        }

        private void PasswordTxt_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            temppass.SelectedText = passwordTxt.Password;
//            new NetworkCredential()
        }
    }
}