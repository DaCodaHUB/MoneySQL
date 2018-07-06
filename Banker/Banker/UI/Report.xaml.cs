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

namespace Banker
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private readonly List<Database.SqlConnect.Bank> _Total;
        private readonly List<Database.SqlConnect.Bank> _Expense;

        public Report(List<Database.SqlConnect.Bank> _total, List<Database.SqlConnect.Bank> _expense)
        {
            InitializeComponent();
            _Total = _total;
            _Expense = _expense;
        }

        private void LastMonth_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MonthlyStats_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExpensesStats_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
