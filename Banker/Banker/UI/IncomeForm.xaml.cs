using System;
using System.Collections.Generic;
using System.Windows;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for IncomeForm.xaml
    /// </summary>
    public partial class IncomeForm
    {
        private readonly int _userId;
        private readonly SqlConnect _sql;
        private List<SqlConnect.Bank> _total;
        private List<SqlConnect.Bank> _expense;
        private decimal _current;

        public IncomeForm(int userId)
        {
            InitializeComponent();
            _sql = new SqlConnect();
            _userId = userId;
            _total = _sql.PullData("total", _userId);
            _expense = _sql.PullData("expense", _userId);

            _current = _total.Count >= 1 ? _total[_total.Count - 1].Money : 0;
            CurrentMoney.Text = _current.ToString("C");
        }

        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            // Get amount of income
            var income = GetMoneyInput();
            if (income <= 0) return;

            // Get current total
            _current = _current + income;

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            _sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
        }

        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            // Get amount of expense
            var expense = GetMoneyInput();
            if (expense <= 0) return;


            // Get category
            string category;
            if (string.IsNullOrWhiteSpace(Category.Text))
            {
                MessageBox.Show(@"You didn't choose category");
                return;
            }
            else
            {
                category = Category.Text;
            }

            // Get current total
            _current = _current - expense;

            // Update the expense list
            var expenseInput = new SqlConnect.Bank(expense, DateTime.Now, category);
            _sql.InsertMoney("expense", _userId, expense, category);
            _expense.Add(expenseInput);

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            _sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
        }

        private decimal GetMoneyInput()
        {
            decimal money = 0;
            if (string.IsNullOrWhiteSpace(Money.Text))
            {
                MessageBox.Show(@"The amount of money can't be empty");
            }
            else
            {
                if (!decimal.TryParse(Money.Text, out money))
                    MessageBox.Show(@"The amount of money has to be a number");
                if (money < 0)
                    MessageBox.Show(@"The amount of money can't be negative");
            }

            return money;
        }

        private void Report_OnClick(object sender, RoutedEventArgs e)
        {
            var report = new Report(_total,_expense);
            report.Show();
        }
    }
}
