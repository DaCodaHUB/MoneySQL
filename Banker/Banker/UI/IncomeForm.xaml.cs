using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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
            DataContext = new TextFields();
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

        private void LastMonth_Click(object sender, RoutedEventArgs e)
        {
            var report = new Lastmonth(_total);
            report.Show();
        }

        private void Monthly_Click(object sender, RoutedEventArgs e)
        {
            var report = new Monthly(_total);
            report.Show();
        }

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            var report = new Expenses(_expense);
            report.Show();
        }



        private void Money_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Decimal:
                case Key.OemPeriod:
                case Key.Enter:
                    e.Handled = false;
                    break;
                default:
                    MessageBox.Show("Please enter only Number.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    e.Handled = true;
                    break;
            }
        }
    }
}