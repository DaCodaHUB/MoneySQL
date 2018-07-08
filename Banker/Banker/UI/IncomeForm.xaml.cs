using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Diagnostics;
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
        private List<SqlConnect.Bank> _random;
        private decimal _current;

        public IncomeForm(int userId)
        {
            InitializeComponent();
            DataContext = new TextFields();
            _sql = new SqlConnect();
            _userId = userId;
            _total = _sql.PullData("total", _userId);
            _expense = _sql.PullData("expense", _userId);

            // For testing
            _random = new RandomListData().generate();
            foreach (var r in _random)
            {
                DataGridExpense.Items.Add(r);
            }

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
            Debug.WriteLine(category);

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
            List<SqlConnect.Bank> lastMonthList = _random.FindAll(elem => elem.Timestamp.Month == DateTime.Now.Month - 1);
            List<KeyValuePair<decimal, decimal>> chartList = new List<KeyValuePair<decimal, decimal>>();

            foreach (var item in lastMonthList)
            {
                chartList.Add(new KeyValuePair<decimal, decimal>(item.Timestamp.Day, item.Money));
            }

            var report = new Lastmonth(chartList);
            report.Show();
        }

        private void Monthly_Click(object sender, RoutedEventArgs e)
        {
            List<KeyValuePair<decimal, decimal>> chartList = monthlyTotals();

            var report = new Monthly(chartList);
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
            valueList.Add(new KeyValuePair<string, decimal>("Education", educationSum));
            valueList.Add(new KeyValuePair<string, decimal>("Entertainment", entertaimentSum));
            valueList.Add(new KeyValuePair<string, decimal>("Transportation", transportationSum));
            valueList.Add(new KeyValuePair<string, decimal>("Food and Drink", foodSum));
            valueList.Add(new KeyValuePair<string, decimal>("Services", serviceSum));
            valueList.Add(new KeyValuePair<string, decimal>("Materials", materialSum));

            var report = new Expenses(valueList);
            report.Show();
        }

        private List<KeyValuePair<decimal, decimal>> monthlyExpenses()
        {
            List<KeyValuePair<decimal, decimal>> valueList = new List<KeyValuePair<decimal, decimal>>();

            for (int i = 1; i <= DateTime.Today.Month; i++)
            {
                List<SqlConnect.Bank> tempList = _random.FindAll(elem => elem.Timestamp.Month == i
                                                                    && elem.Timestamp.Year == DateTime.Today.Year);
                decimal sum = tempList.Sum(item => item.Money);
                Debug.WriteLine(sum);
                valueList.Add(new KeyValuePair<decimal, decimal>(i, sum));

            }
            return valueList;
        }

        private List<KeyValuePair<decimal, decimal>> monthlyTotals()
        {
            List<KeyValuePair<decimal, decimal>> valueList = new List<KeyValuePair<decimal, decimal>>();

            for (int i = 1; i <= DateTime.Today.Month; i++)
            {
                List<SqlConnect.Bank> tempList = _random.FindAll(elem => elem.Timestamp.Month == i
                                                                    && elem.Timestamp.Year == DateTime.Today.Year);
                decimal value = tempList[0].Money;
                valueList.Add(new KeyValuePair<decimal, decimal>(i, value));

            }
            return valueList;
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