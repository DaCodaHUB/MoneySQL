using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Banker.Graph
{
    /// <summary>
    /// Interaction logic for Expenses_UC.xaml
    /// </summary>
    public partial class Expenses_UC : UserControl
    {
        public SeriesCollection SeriesCollection { get; }

        public Expenses_UC(List<KeyValuePair<string, decimal>> _expense)
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();

            foreach (var item in _expense)
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
