using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Diagnostics;
using Banker.Database;
using Banker.Graph;

namespace Banker
{
    /// <summary>
    /// Interaction logic for IncomeForm.xaml
    /// </summary>
    public partial class IncomeForm
    {
        private readonly int _userId;
        private readonly SqlConnect _sql;
        private readonly List<SqlConnect.Bank> _total;
        private readonly List<SqlConnect.Bank> _expense;
        private readonly List<SqlConnect.Bank> _random;
        private readonly ObservableCollection<SqlConnect.Bank> _observableDataBanks;
        private decimal _current;

        public IncomeForm(int userId)
        {
            InitializeComponent();
            DataContext = new TextFields();
            _sql = new SqlConnect();
            _userId = userId;
            _total = _sql.PullData("total", _userId);
            _expense = _sql.PullData("expense", _userId);


            _observableDataBanks = new ObservableCollection<SqlConnect.Bank>(_expense);


            // For testing
            _random = new RandomListData().Generate();

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

            MessageBox.Show(@"You successfully added an income to your bank");

            // Get current total
            _current = _current + income;

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            _sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
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

            MessageBox.Show(@"You successfully added an expense to your bank");

            Debug.WriteLine(category);

            // Get current total
            _current = _current - expense;

            // Update the expense list
            var expenseInput = new SqlConnect.Bank(expense, DateTime.Now, category);

            _sql.InsertMoney("expense", _userId, expense, category);
            _expense.Add(expenseInput);
            _observableDataBanks.Add(expenseInput);

            // Update Total list and database
            var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
            _sql.InsertMoney("total", _userId, _current);
            _total.Add(currentTotal);

            // Show current total
            CurrentMoney.Text = _current.ToString("C");
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

        private void LastMonth_Click(object sender, RoutedEventArgs e)
        {
            var chartList = new List<KeyValuePair<int, decimal>>();

            for (int i = 1; i <= 31; i++)
            {
                var tempList = new List<SqlConnect.Bank>();

                if (DateTime.Today.Month > 1)
                {
                    tempList = _random.FindAll(elem => elem.Timestamp.Month == DateTime.Now.Month - 1 && elem.Timestamp.Day == i);
                } else
                {
                    tempList = _random.FindAll(elem => elem.Timestamp.Month == 12 && elem.Timestamp.Year == DateTime.Today.Year - 1 
                                                && elem.Timestamp.Day == i);
                }

                var sum = tempList.Sum(item => item.Money);
                chartList.Add(new KeyValuePair<int, decimal>(i, sum));
            }

            if (chartList.Count > 0)
            {
                var report = new Lastmonth(chartList);
                report.Show();
            } else
            {
                MessageBox.Show("No data last month");
            }
        }

        private void Monthly_Click(object sender, RoutedEventArgs e)
        {
            var valueList = new List<KeyValuePair<int, decimal>>();

            for (int i = 1; i <= DateTime.Today.Month; i++)
            {
                var tempList = _random.FindAll(elem => elem.Timestamp.Month == i
                                                                         && elem.Timestamp.Year == DateTime.Today.Year);
                decimal value = 0;
                if (tempList.Count > 0)
                {
                    value = tempList[0].Money;
                }

                valueList.Add(new KeyValuePair<int, decimal>(i, value));
            }

            var report = new Monthly(valueList);
            report.Show();
        }

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            List<SqlConnect.Bank> tempList = _expense.FindAll(elem => elem.Category == "Education");
            decimal educationSum = tempList.Sum(item => item.Money);

            tempList = _expense.FindAll(elem => elem.Category == "Entertainment");
            decimal entertaimentSum = tempList.Sum(item => item.Money);

            tempList = _expense.FindAll(elem => elem.Category == "Transportation");
            decimal transportationSum = tempList.Sum(item => item.Money);

            tempList = _expense.FindAll(elem => elem.Category == "Food and Drink");
            decimal foodSum = tempList.Sum(item => item.Money);

            tempList = _expense.FindAll(elem => elem.Category == "Services");
            decimal serviceSum = tempList.Sum(item => item.Money);

            tempList = _expense.FindAll(elem => elem.Category == "Materials");
            decimal materialSum = tempList.Sum(item => item.Money);

            List<KeyValuePair<string, decimal>> valueList = new List<KeyValuePair<string, decimal>>();
            if (educationSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Education", educationSum)); }
            if (entertaimentSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Entertainment", entertaimentSum)); }
            if (transportationSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Transportation", transportationSum)); }
            if (foodSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Food and Drink", foodSum)); }
            if (serviceSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Services", serviceSum)); }
            if (materialSum > 0) { valueList.Add(new KeyValuePair<string, decimal>("Materials", materialSum)); }

            var report = new Expenses(valueList);
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
                var timestamp = item.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

                _observableDataBanks.Remove(item);
                _current += item.Money;
                _expense.Remove(item);
                var currentTotal = new SqlConnect.Bank(_current, DateTime.Now);
                _sql.InsertMoney("total", _userId, _current);
                _total.Add(currentTotal);
                _sql.DeleteMoney("expense", timestamp);
            }

            CurrentMoney.Text = _current.ToString("C");
        }
    }
}