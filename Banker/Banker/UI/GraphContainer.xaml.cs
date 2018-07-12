using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Banker.Charts;
using Banker.Database;

namespace Banker
{
    /// <summary>
    /// Interaction logic for DataForm.xaml
    /// </summary>
    public partial class GraphContainer
    {
        private readonly List<KeyValuePair<string, decimal>> _expenseCalculated;
        private readonly List<KeyValuePair<int, decimal>> _monthlyTotalCalculated;
        private readonly List<KeyValuePair<int, decimal>> _monthlyExpenseCalculated;
        private readonly List<KeyValuePair<int, decimal>> _lastmonthCalculated;


        public GraphContainer(List<SqlConnect.Bank> expense, List<SqlConnect.Bank> total)
        {
            InitializeComponent();
            _expenseCalculated = Expenses_Calculate(expense);
            _monthlyTotalCalculated = MonthlyTotal_Calculate(total);
            _monthlyExpenseCalculated = MonthlyExpense_Calculate(expense);
            _lastmonthCalculated = LastMonth_Calculate(total); // Testing: expense -> total
            ChartCombo.SelectedIndex = 0;
        }


        private void Expenses_OnSelected(object sender, RoutedEventArgs e)
        {
            if (_expenseCalculated.Count > 0)
            {
                ChartContainer.Children.Clear();
                var report = new ExpensesUc(_expenseCalculated);
                ChartContainer.Children.Add(report);
            }
            else
            {
                MessageBox.Show("There is no data available for expenses");
            }
        }

        private void Monthly_OnSelected(object sender, RoutedEventArgs e)
        {
            if (_monthlyTotalCalculated.FindAll(a => a.Value != 0).Count > 0)
            {
                ChartContainer.Children.Clear();
                var report = new MonthlyUc(_monthlyTotalCalculated, _monthlyExpenseCalculated);
                ChartContainer.Children.Add(report);
            }
            else
            {
                MessageBox.Show("There is no data available for monthly");
            }
        }

        private void LastMonth_OnSelected(object sender, RoutedEventArgs e)
        {
            if (_lastmonthCalculated.FindAll(a => a.Value != 0).Count > 0)
            {
                ChartContainer.Children.Clear();
                var report = new LastMonthUc(_lastmonthCalculated);
                ChartContainer.Children.Add(report);
            }
            else
            {
                MessageBox.Show("There is no data available for last month");
            }
        }

        private static List<KeyValuePair<int, decimal>> MonthlyTotal_Calculate(List<SqlConnect.Bank> monthly)
        {
            var valueList = new List<KeyValuePair<int, decimal>>();

            for (var i = 1; i <= DateTime.Today.Month; i++)
            {
                var tempList = monthly.FindAll(elem => elem.Timestamp.Month == i
                                                       && elem.Timestamp.Year == DateTime.Today.Year);
                decimal value = 0;
                if (tempList.Count > 0)
                {
                    value = tempList[tempList.Count - 1].Money;
                }

                valueList.Add(new KeyValuePair<int, decimal>(i, value));
            }

            return valueList;
        }

        private static List<KeyValuePair<int, decimal>> MonthlyExpense_Calculate(List<SqlConnect.Bank> monthly)
        {
            var valueList = new List<KeyValuePair<int, decimal>>();

            for (var i = 1; i <= DateTime.Today.Month; i++)
            {
                var tempList = monthly.FindAll(elem => elem.Timestamp.Month == i
                                                       && elem.Timestamp.Year == DateTime.Today.Year);

                var sum = tempList.Sum(item => item.Money);

                valueList.Add(new KeyValuePair<int, decimal>(i, sum));
            }

            return valueList;
        }

        private static List<KeyValuePair<string, decimal>> Expenses_Calculate(List<SqlConnect.Bank> expense)
        {
            List<SqlConnect.Bank> tempList = expense.FindAll(elem => elem.Category == "Education");
            decimal educationSum = tempList.Sum(item => item.Money);

            tempList = expense.FindAll(elem => elem.Category == "Entertainment");
            decimal entertaimentSum = tempList.Sum(item => item.Money);

            tempList = expense.FindAll(elem => elem.Category == "Transportation");
            decimal transportationSum = tempList.Sum(item => item.Money);

            tempList = expense.FindAll(elem => elem.Category == "Food and Drink");
            decimal foodSum = tempList.Sum(item => item.Money);

            tempList = expense.FindAll(elem => elem.Category == "Services");
            decimal serviceSum = tempList.Sum(item => item.Money);

            tempList = expense.FindAll(elem => elem.Category == "Materials");
            decimal materialSum = tempList.Sum(item => item.Money);

            var valueList = new List<KeyValuePair<string, decimal>>();
            if (educationSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Education", educationSum));
            }

            if (entertaimentSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Entertainment", entertaimentSum));
            }

            if (transportationSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Transportation", transportationSum));
            }

            if (foodSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Food and Drink", foodSum));
            }

            if (serviceSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Services", serviceSum));
            }

            if (materialSum > 0)
            {
                valueList.Add(new KeyValuePair<string, decimal>("Materials", materialSum));
            }

            return valueList;
        }

        private static List<KeyValuePair<int, decimal>> LastMonth_Calculate(List<SqlConnect.Bank> lastMonth)
        {
            var chartList = new List<KeyValuePair<int, decimal>>();

            for (var i = 1; i <= 31; i++)
            {
                var tempList = new List<SqlConnect.Bank>();

                if (DateTime.Today.Month > 1)
                {
                    tempList = lastMonth.FindAll(elem =>
                        elem.Timestamp.Month == DateTime.Now.Month - 1 && elem.Timestamp.Day == i);
                }
                else
                {
                    tempList = lastMonth.FindAll(elem =>
                        elem.Timestamp.Month == 12 && elem.Timestamp.Year == DateTime.Today.Year - 1
                                                   && elem.Timestamp.Day == i);
                }

                var sum = tempList.Sum(item => item.Money);
                chartList.Add(new KeyValuePair<int, decimal>(i, sum));
            }

            return chartList;
        }
    }
}