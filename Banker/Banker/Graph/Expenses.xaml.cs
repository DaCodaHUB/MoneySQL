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
//        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public Expenses(List<KeyValuePair<string, decimal>> _expense)
        {
            InitializeComponent();


            SeriesCollection = new SeriesCollection();

            foreach (var item in _expense)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<decimal> {item.Value},
                    DataLabels = true
                });
            }

//            PointLabel = chartPoint => $"{chartPoint.Participation:P}";
            DataContext = this;
        }
    }
}