using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Banker.Charts
{
    /// <summary>
    /// Interaction logic for Expenses_UC.xaml
    /// </summary>
    public partial class ExpensesUc : UserControl
    {
        public SeriesCollection SeriesCollection { get; }

        public ExpensesUc(List<KeyValuePair<string, decimal>> expense)
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();

            foreach (var item in expense)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<decimal> { item.Value },
                    DataLabels = true
                });
            }

            //            PointLabel = chartPoint => $"{chartPoint.Participation:P}";
            DataContext = this;
        }
    }
}
