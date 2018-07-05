using System;
using System.Windows.Forms;
using moneyManage.Database;
using moneyManage.UI;

namespace moneyManage
{
    public partial class SignIn : Form
    {
        private readonly SqlConnect _sql;

        public SignIn()
        {
            this._sql = new SqlConnect();
            InitializeComponent();
        }

        
        private void SignIn_Click(object sender, EventArgs e)
        {
            var username = usernameTxt.Text;
            var pass = passwordTxt.Text;
    
            var result = _sql.VerifyUser(username,pass);
            if (!result.Valid)
                MessageBox.Show($@"This {username} is not existed or password is incorrect");
            else
            {
                this.Hide();
                var form = new Form1(result.Id);
                Console.WriteLine(result.Id);
                Console.WriteLine(_sql.PullExpenses(result.Id));
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            // this.Hide();
            var username = usernameTxt.Text;
            var pass = passwordTxt.Text;
  
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


            // this.Show();
        }
    }
}