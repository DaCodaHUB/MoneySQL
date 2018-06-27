using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace moneyManage
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string userid = textBox1.Text;
            string pass = textBox2.Text;
            var form = new Form1(userid,pass);
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string userid = textBox1.Text;
            string pass = textBox2.Text;
            var form = new Form1(userid, pass);
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}
