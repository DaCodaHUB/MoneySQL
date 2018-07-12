using System;
using System.Collections.Generic;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace Banker.Charts
{
    /// <summary>
    /// Interaction logic for Monthly_UC.xaml
    /// </summary>
    public partial class MonthlyUc
    {
        public MonthlyUc(List<KeyValuePair<int, decimal>> totalList, List<KeyValuePair<int, decimal>> expenseList)
        {
            InitializeComponent();

            var totalValues = new ChartValues<decimal>();
            var expenseValues = new ChartValues<decimal>();

            Labels = new string[totalList.Count];

            for (int i = 0; i < totalList.Count; i++)
            {
                Labels[i] = totalList[i].Key.ToString();
                totalValues.Add(totalList[i].Value);
                expenseValues.Add(expenseList[i].Value);
            }

            SeriesCollection = new SeriesCollection {
                new LineSeries
                {
                    Title = "Monthly Stats",
                    Values = totalValues,
                    LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                    PointGeometry = null,
                    PointForeground = Brushes.Blue
                },
                new LineSeries
                {
                    Title = "Monthly Expenses",
                    Values = expenseValues,
                    LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                    PointGeometry = null,
                    PointForeground = Brushes.Red
                },
            };

            YFormatter = value => value.ToString("C");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
