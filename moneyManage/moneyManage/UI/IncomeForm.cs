using System;
using System.Collections.Generic;
using System.Windows.Forms;
using moneyManage.Database;
using System.Diagnostics;

namespace moneyManage.UI
{
    public partial class Form1 : Form
    {
        private readonly int _userId;

        private SqlConnect sql;
        private List<SqlConnect.Bank> _total;
        private List<SqlConnect.Bank> _expense;
        private decimal _current;

        public Form1(int userId)
        {
            InitializeComponent();
            _userId = userId;
            sql = new SqlConnect();
            _total = sql.PullData("total", _userId);
            _expense = sql.PullData("expense", _userId);

            _current = _total.Count >= 1 ? _total[_total.Count - 1].Money : 0;

            Debug.WriteLine(_current);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentMoney.Text = _current.ToString("C");
        }

        private void Report_Click(object sender, EventArgs e)
        {
            var report = new Report(_total, _expense);
            report.Show();
        }

        private void Income_Click(object sender, EventArgs e)
        {
            // Get amount of income
            var income = GetMoneyInput();
            if (income == 0) return;

            // Get current total
            _current = _current + income;

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
        }


        private void Spend_Click(object sender, EventArgs e)
        {
            // Get amount of expense
            var expense = GetMoneyInput();
            if (expense == 0) return;


            // Get category
            string category;
            if (string.IsNullOrWhiteSpace(catagoryList.Text))
            {
                MessageBox.Show(@"You didn't choose category");
                return;
            }
            else
            {
                category = catagoryList.Text;
            }

            // Get current total
            _current = _current - expense;

            // Update the expense list
            var expenseInput = new SqlConnect.Bank(expense, DateTime.Now, category);
            sql.InsertMoney("expense", _userId, expense, category);
            _expense.Add(expenseInput);

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
        }

        private decimal GetMoneyInput()
        {
            decimal income = 0;
            if (string.IsNullOrWhiteSpace(MoneyTxt.Text))
            {
                MessageBox.Show(@"The amount of money can't be empty");
            }
            else
            {
                income = decimal.Parse(MoneyTxt.Text);
                if (income < 0)
                {
                    MessageBox.Show(@"The amount of money can't be negative");
                }
            }

            return income;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}