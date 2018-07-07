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
    /// Interaction logic for Expenses.xaml
    /// </summary>
    public partial class Expenses : Window
    {
        private readonly List<KeyValuePair<string, decimal>> _Expense;

        public Expenses(List<KeyValuePair<string, decimal>> _expense)
        {
            InitializeComponent();
            this._Expense = _expense;
            DataContext = _expense;
        }
    }

}
