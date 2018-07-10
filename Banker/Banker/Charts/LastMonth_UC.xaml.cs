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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace Banker.Charts
{
    /// <summary>
    /// Interaction logic for LastMonth_UC.xaml
    /// </summary>
    public partial class LastMonth_UC : UserControl
    {
        public LastMonth_UC(List<KeyValuePair<int, decimal>> valueList)
        {
            InitializeComponent();

            var values = new ChartValues<decimal>();

            Labels = new string[valueList.Count];

            var i = 0;
            foreach (var item in valueList)
            {
                Labels[i] = item.Key.ToString();
                values.Add(item.Value);
                i++;
            }

            SeriesCollection = new SeriesCollection {
                new LineSeries
                {
                    Title = "Monthly Stats",
                    Values = values,
                    LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                    PointGeometry = null,
                    PointForeground = Brushes.Blue
                }
            };

            YFormatter = value => value.ToString("C");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
