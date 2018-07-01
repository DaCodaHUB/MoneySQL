using System;
using System.Windows.Forms;
using moneyManage.Database;

namespace moneyManage
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            string userid = usernameTxt.Text;
            string pass = passwordTxt.Text;
            MessageBox.Show(userid + " " + pass);
            var form = new Form1(userid, pass);
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            string userid = usernameTxt.Text;
            string pass = passwordTxt.Text;
            SqlConnect sql = new SqlConnect();
            var replayCode = sql.CreateNewUser(userid, pass);

            if (replayCode == 1)
            {
                MessageBox.Show($@"Username {userid} is existed");
            }
            else
            {

            }

            var form = new Form1(userid, pass);
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}