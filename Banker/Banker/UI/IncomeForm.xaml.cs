using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for IncomeForm.xaml
    /// </summary>
    public partial class IncomeForm
    {
        private readonly int _userId;
        private readonly List<SqlConnect.Bank> _total;
        private readonly List<SqlConnect.Bank> _expense;
        private readonly List<SqlConnect.Bank> _random; // For testing
        private readonly ObservableCollection<SqlConnect.Bank> _observableDataBanks;
        private decimal _current;

        public IncomeForm(int userId)
        {
            InitializeComponent();
            DataContext = new TextFields();
            _userId = userId;
            _total = SqlConnect.PullData("total", _userId);
            _expense = SqlConnect.PullData("expense", _userId);


            _observableDataBanks = new ObservableCollection<SqlConnect.Bank>(_expense);

            _random = new RandomListData().Generate(); // For testing

            DataGridExpense.ItemsSource = _observableDataBanks;

            _current = _total.Count >= 1 ? _total[_total.Count - 1].Money : 0;
            CurrentMoney.Text = _current.ToString("C");
        }

        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            Income.IsEnabled = false;
            // Get amount of income
            var income = GetMoneyInput();
            if (income <= 0)
            {
                Income.IsEnabled = true;
                return;
            }

            // Get current total
            _current = _current + income;

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            SqlConnect.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
            MessageBox.Show(@"You successfully added an income to your bank");
            Income.IsEnabled = true;
        }

        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            Spend.IsEnabled = false;
            // Get amount of expense
            var expense = GetMoneyInput();
            if (expense <= 0)
            {
                Spend.IsEnabled = true;
                return;
            }

            // Get category
            string category;
            if (string.IsNullOrWhiteSpace(Category.Text))
            {
                MessageBox.Show(@"You didn't choose category");
                Spend.IsEnabled = true;
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

            SqlConnect.InsertMoney("expense", _userId, expense, category);
            _expense.Add(expenseInput);
            _observableDataBanks.Add(expenseInput);

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            SqlConnect.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
            MessageBox.Show(@"You successfully added an expense to your bank");
            Spend.IsEnabled = true;
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


        private void ViewCharts_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var graph = new GraphContainer(_expense, _random); // Testing : _total -> _random
            graph.Closed += (s, args) => Show();
            graph.Show();
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


        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var removeItem = _observableDataBanks.Where(x => x.Selected).ToList();

            if (removeItem.Count == 0)
            {
                MessageBox.Show("Check a box to delete a record.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            foreach (var item in removeItem)
            {
//                var timestamp = item.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

                _observableDataBanks.Remove(item);
                _current += item.Money;
                _expense.Remove(item);
                var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
                SqlConnect.InsertMoney("total", _userId, _current);
                _total.Add(currentTotal);
                SqlConnect.DeleteMoney("expense", item.Id, _userId);
            }

            CurrentMoney.Text = _current.ToString("C");
        }
    }
}