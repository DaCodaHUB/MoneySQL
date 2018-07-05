using System;
using System.Collections.Generic;
using System.Windows.Forms;
using moneyManage.Database;
using System.Diagnostics;

namespace moneyManage.UI
{
    public partial class Form1 : Form
    {
        private int _userId;
        private SqlConnect sql;
        // TODO: Loaded data in _expenses and _totals
        private TotalStruct totalData;
        private ExpenseStruct expenseData;

        public Form1(int userId)
        {
            InitializeComponent();
            this._userId = userId;
            sql = new SqlConnect();
            totalData = new TotalStruct();
            expenseData = new ExpenseStruct();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            expenseData = sql.PullExpenses(_userId);
            totalData = sql.PullTotal(_userId);

            Debug.WriteLine("Loading");
            Debug.WriteLine(totalData.Current.money);
        }

        private void Report_Click(object sender, EventArgs e)
        {
            var report = new Report(totalData, expenseData);
            report.Show();
        }

        private void Income_Click(object sender, EventArgs e)
        {
            // Get amount of income
            decimal income = 0;
            if (string.IsNullOrEmpty(this.MoneyTxt.Text))
            {
                MessageBox.Show("The amount of money can't be empty");
                return;
            }
            else
            {
                income = decimal.Parse(this.MoneyTxt.Text);
            }
            
            // Get current total
            decimal current = totalData.Current.money + income;

            // Update Total list and database
            totalData.Insert(current, DateTime.Now);

            // Show current total
            CurrentMoney.Text = current.ToString();
        }

        private void Spend_Click(object sender, EventArgs e)
        {
            // Get amount of expense
            decimal expense = 0;
            if (string.IsNullOrWhiteSpace(this.MoneyTxt.Text))
            {
                MessageBox.Show(@"The amount of money can't be empty");
                return;
            }
            else
            {
                expense = decimal.Parse(this.MoneyTxt.Text);
            }

            // Get category
            string category = "";
            if (string.IsNullOrWhiteSpace(this.catagoryList.Text))
            {
                MessageBox.Show(@"You didn't choose category");
                return;
            }
            else
            {
                category = this.catagoryList.Text;
            }

            // Get current total
            decimal current = totalData.Current.money - expense;

            // Update current total
            //TotalStruct.Total newCurrent = new TotalStruct.Total(current, DateTime.Now);
            //totalData.Current = newCurrent;

            // Update the expense list
            expenseData.Insert(category, expense, DateTime.Now);

            // Update Total list and database
            totalData.Insert(current, DateTime.Now);

            // Show current total
            CurrentMoney.Text = current.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}