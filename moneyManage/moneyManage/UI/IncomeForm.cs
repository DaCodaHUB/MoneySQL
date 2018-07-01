using System;
using System.Windows.Forms;

namespace moneyManage.UI
{
    public partial class Form1 : Form
    {
        public Form1(string user, string pass)
        {
            InitializeComponent();
            username = user;
            password = pass;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Report_Click(object sender, EventArgs e)
        {
            var report = new Report();
            report.Show();
        }

        private void Income_Click(object sender, EventArgs e)
        {

        }

        private void Spend_Click(object sender, EventArgs e)
        {

        }
    }
}
