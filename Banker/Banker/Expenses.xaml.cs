using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace Banker
{
    /// <summary>
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Window
    {
        private readonly List<KeyValuePair<string, decimal>> _Expense;

        public Expenses(List<KeyValuePair<string, decimal>> _expense)
        {
            InitializeComponent();
            this._Expense = _expense;

            SeriesCollection = new SeriesCollection();

            foreach (var item in _expense)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<decimal> {item.Value},
                    DataLabels = true,
                });
            }

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        private void Chart_OnDataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }

}
