using System;
using System.Windows.Forms;
using moneyManage.Database;

namespace moneyManage.UI
{
    public partial class Form1 : Form
    {
        // Todo: might not need to pass in user and pass but need TotalStruct and ExpenseStruct
        public Form1(string userID)
        {
            InitializeComponent();
            this.userID = userID;
            totalData = new TotalStruct();
            expenseData = new ExpenseStruct();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Report_Click(object sender, EventArgs e)
        {
            var report = new Report(totalData, expenseData);
            report.Show();
        }

        private void Income_Click(object sender, EventArgs e)
        {
            // Get amount of income
            int income = 0;
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("The amount of money can't be empty");
                return;
            }
            else
            {
                income = Int32.Parse(this.textBox1.Text);
            }

            // Get current total
            int current = totalData.Current.money + income;

            // Update current total
            // (?) Only update the database after the program is close
            TotalStruct.Total newCurrent = new TotalStruct.Total(totalData.Current.userid, DateTime.Now, current);
            totalData.Current = newCurrent;

            // Show current total
            textBox3.Text = current.ToString();
        }

        private void Spend_Click(object sender, EventArgs e)
        {
            // Get amount of expense
            int expense = 0;
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("The amount of money can't be empty");
                return;
            } else
            {
                expense = Int32.Parse(this.textBox1.Text);
            }

            // Get category
            string category = "";
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("You didn't choose category");
                return;
            }
            else
            {
                category = this.comboBox1.Text;
            }

            // Get current total
            int current = totalData.Current.money - expense;

            // Update current total
            // (?) Only update the database after the program is close
            TotalStruct.Total newCurrent = new TotalStruct.Total(totalData.Current.userid, DateTime.Now, current);
            totalData.Current = newCurrent;

            // Update the expense list
            expenseData.Insert(expenseData.UserID, DateTime.Now, category, expense);

            // Show current total
            textBox3.Text = current.ToString();
        }
    }
}
