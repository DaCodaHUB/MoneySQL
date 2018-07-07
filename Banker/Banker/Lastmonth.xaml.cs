﻿using System;
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
    /// Interaction logic for Lastmonth.xaml
    /// </summary>
    public partial class Lastmonth : Window
    {
        private readonly List<KeyValuePair<decimal, decimal>> _Total;

        public Lastmonth(List<KeyValuePair<decimal, decimal>> _total)
        {
            InitializeComponent();
            this._Total = _total;
            lineChart.DataContext = _total;
        }
    }

}