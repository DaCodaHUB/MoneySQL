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
        private List<SqlConnect.Bank> _total;
        private List<SqlConnect.Bank> _expense;
        private Decimal _current;

        public Form1(int userId)
        {
            InitializeComponent();
            this._userId = userId;
            sql = new SqlConnect();
            _total = sql.PullTotal(_userId);
            _expense = sql.PullExpenses(_userId);

            if (_total.Count >= 1) {
                _current = _total[_total.Count - 1].Money;
            } else
            {
                _current = 0;
            }

            Debug.WriteLine(_current);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
//            _expense = 
//            _total = 

            Debug.WriteLine("Loading");
        }

        private void Report_Click(object sender, EventArgs e)
        {
            var report = new Report(_total, _expense);
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
            _current = _current + income;

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            sql.InsertMoneyTotal(_userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString();
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
            _current = _current - expense;

            // Update the expense list
            var expenseInput = new SqlConnect.Bank(expense, DateTime.Now, category);
            sql.InsertMoneyExpense(_userId, category, expense);
            _expense.Add(expenseInput);

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            sql.InsertMoneyTotal(_userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Close Form");
            foreach (var t in _total)
            {
                Console.WriteLine($@"{t.Timestamp} {t.Money}");
            }
        }
    }
}