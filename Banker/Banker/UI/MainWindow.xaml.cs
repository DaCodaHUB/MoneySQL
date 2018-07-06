using System.Windows;
using Banker.Database;

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
        }

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;
            var pass = PasswordTxt.Password;

            var result = _sql.VerifyUser(username, pass);
            if (!result.Valid)
                MessageBox.Show($@"This {username} is not existed or password is incorrect");
            else
            {
                this.Hide();
                var incomeForm = new IncomeForm(result.Id);
                incomeForm.Closed += (s, args) => this.Close();
                incomeForm.Show();
            }
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameTxt.Text;
            var pass = PasswordTxt.Password;

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
    }
}
